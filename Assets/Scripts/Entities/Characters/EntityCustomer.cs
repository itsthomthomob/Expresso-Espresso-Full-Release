using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

[Serializable]
public class CustomerVars
{
    public int MyCustomerIDData;
    public string MyStateData;
    public string MyItemData;
}

public class EntityCustomer : EntityBase
{
    public float maxRotation = 5f;
    public float rotationSpeed = 6.5f;
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Characters/Character002"));
        SetEntityPriority(EntityPriority.Characters);
        SetEntityName("Customer");
    }

    public enum State 
    { 
        WaitingForCafe, GoingToRegister, DisplayText, GoingToEmptyChair, SitDown, WaitAtChair, WaitAtRandomLocation, GoingToDrink, LeavingCafe
    }

    [SerializeField] private State CurrentState = State.WaitingForCafe;
    [SerializeField] public int MyCustomerID;
    [SerializeField] private EntityCoffee MyCoffee;
    [SerializeField] private float Range = 3.0f;
    [SerializeField] private MenuItem MyItem;
    [SerializeField] private EntityConcrete MyConcrete;
    EntityConcrete[] AllConcrete;
    MenuManagementSystem GetMenu;
    MenuItem[] ScanMenu;

    CafeEconomySystem GetECO;
    bool didReview;

    EntityBase MyChair;
    EntityRegister Register;

    TileConstruction getTiles;

    List<Image> OrderedTexts = new List<Image>();
    GameObject MyTextBubble;

    Stopwatch TextWatch = new Stopwatch();

    public TimeManager GetTime;
    public float Speed;

    private void Awake()
    {
        GetTime = FindObjectOfType<TimeManager>();
        // Get coffee menu
        GetMenu = FindObjectOfType<MenuManagementSystem>();
        ScanMenu = FindObjectsOfType<MenuItem>();
        GetECO = FindObjectOfType<CafeEconomySystem>();
        getTiles = FindObjectOfType<TileConstruction>();
        didReview = false;

        // Load text bubbles
        string[] FileNames = new string[] { "Customer-Ordered0.png", "Customer-Ordered1.png" };
        UnityEngine.Debug.Log(FileNames[0]);
        for (int i = 0; i < FileNames.Length; i++)
        {
            Image LoadSprite = Resources.Load("Sprites/UI/TextBubbles/Customer/Ordered" + FileNames[i]) as Image;
            if (!OrderedTexts.Contains(LoadSprite))
            {
                OrderedTexts.Add(LoadSprite);
            }
        }
    }

    private void Start()
    {
        AllConcrete = FindObjectsOfType<EntityConcrete>();
    }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * rotationSpeed));
        }
        else 
        { 
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        Speed = GetTime.scale * 0.25f;

        switch (CurrentState)
        {
            case State.WaitingForCafe:
                OnWaitingForCafe();
                break;
            case State.GoingToRegister:
                OnGoingToRegister();
                break;
            case State.DisplayText:
                OnDisplayText();
                break;
            case State.GoingToEmptyChair:
                OnGoingToEmptyChair();
                break;
            case State.SitDown:
                OnSitDown();
                break;
            case State.WaitAtChair:
                OnWaitAtChair();
                break;
            case State.WaitAtRandomLocation:
                OnWaitAtRandomLocation();
                break;
            case State.GoingToDrink:
                OnGoingToDrink();
                break;
            case State.LeavingCafe:
                OnLeavingCafe();
                break;
        }
    }

    private void OnWaitingForCafe() 
    {
        if (!IsMoving)
        {
            int index = UnityEngine.Random.Range(0, AllConcrete.Length);
            MenuItem findItem = FindObjectOfType<MenuItem>();
            if (MyConcrete == null)
            {
                MyConcrete = AllConcrete[index];
            }
            else 
            {
                if (getTiles.AllRegisters.Count > 0 && findItem != null)
                {
                    // Player has register and menu item, go to register
                    CurrentState = State.GoingToRegister;
                }
                else if ((Position - MyConcrete.Position).magnitude < Range)
                {
                    // At my concrete, choose another concrete
                    MyConcrete = AllConcrete[index];
                }
                else if (findItem == null)
                {
                    // menu item does not exist
                    bool found = Grid.Pathfind(Position, MyConcrete.Position, IsPassable, out Vector2Int next);
                    if (found)
                    {
                        Move(next, Speed);
                    }
                }
                else if (getTiles.AllRegisters.Count > 0)
                {
                    // a register does not exist
                    bool found = Grid.Pathfind(Position, MyConcrete.Position, IsPassable, out Vector2Int next);
                    if (found)
                    {
                        Move(next, Speed);
                    }
                }
                else 
                {
                    bool found = Grid.Pathfind(Position, MyConcrete.Position, IsPassable, out Vector2Int next);
                    if (found)
                    {
                        Move(next, Speed);
                    }
                }
            }
        }
    }

    private void OnGoingToRegister()
    {
        if (!IsMoving)
        {
            for (int i = 0; i < getTiles.AllRegisters.Count; i++)
            {
                if (getTiles.AllRegisters[i].GetCustomer() == null)
                {
                    Register = getTiles.AllRegisters[i];
                    Register.SetCustomer(this);
                    break;
                }
                else 
                {
                    // Go to customer
                    if ((getTiles.AllRegisters[i].GetCustomer().Position - getTiles.AllRegisters[i].Position).magnitude < Range)
                    {
                        bool found = Grid.Pathfind(Position, getTiles.AllRegisters[i].GetCustomer().Position, IsPassable, out Vector2Int next);
                        if (found)
                        {
                            Move(next, Speed);
                        }
                    }
                }
            }

            if (Register == null)
            {
                // Do nothing, wait for register
            }
            else if ((Position - Register.Position).magnitude < Range)
            {
                CurrentState = State.DisplayText;
                UnityEngine.Debug.LogWarning("At register");
            }
            else
            {
                bool found = Grid.Pathfind(Position, Register.Position, IsPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, Speed);
                }
            }
        }
    }

    private void OnDisplayText()
    {
        if (!TextWatch.IsRunning)
        {
            TextWatch.Start();
        }
        // spawn text bubble
        if (MyTextBubble == null)
        {
            GameObject textBubble = Instantiate(Resources.Load<GameObject>("Sprites/UI/TextBubbles/TextBubble"));
            MyTextBubble = textBubble;
            textBubble.transform.position = new Vector3(transform.position.x, transform.position.y + 50, -10);
            textBubble.transform.SetParent(this.transform);

            // get random customer text
            int index = UnityEngine.Random.Range(0, (OrderedTexts.Count));

            try
            {

            textBubble.transform.GetChild(0).GetComponent<Image>().sprite = OrderedTexts[index].sprite;
            UnityEngine.Debug.Log("Customer: " + index + "Max: " + (OrderedTexts.Count));
            }
            catch (Exception e) 
            { 
            
            }
        }
        else
        {
            if (TextWatch.Elapsed >= new TimeSpan(0, 0, 2))
            {
                if (MyItem == null)
                {
                    int ChooseItem = UnityEngine.Random.Range(0, GetMenu.MenuItems.Count);
                    MyItem = GetMenu.MenuItems[ChooseItem];
                    GetECO.CurrentProfits = GetECO.CurrentProfits + MyItem.GetPrice();
                    GetECO.CurrentExpenses = GetECO.CurrentExpenses + MyItem.GetExpense();

                    StoreLevelManager LevelManager = FindObjectOfType<StoreLevelManager>();
                    LevelManager.CustomerAddXP();
                }
                else 
                { 
                    // Unlink register
                    Register.SetCustomerToNone();
                    Register = null;

                    // Go to chair
                    Destroy(MyTextBubble);


                    CurrentState = State.GoingToEmptyChair;
                    TextWatch.Reset();
                }
            }
        }
    }
    private void OnGoingToEmptyChair()
    {
        if (!IsMoving)
        {
            if (MyChair == null)
            {
                EntityBarstool[] AllBarstools = FindObjectsOfType<EntityBarstool>();
                EntityChairGrey[] AllGreyChairs = FindObjectsOfType<EntityChairGrey>();
                EntityChairRed[] AllRedChairs = FindObjectsOfType<EntityChairRed>();
                EntityChairRough[] AllRoughChairs = FindObjectsOfType<EntityChairRough>();
                EntityChairSmooth[] AllSmoothChairs = FindObjectsOfType<EntityChairSmooth>();

                // Check all barstools
                for (int i = 0; i < AllBarstools.Length; i++)
                {
                    bool found = Grid.Pathfind(Position, AllBarstools[i].Position, IsPassable, out Vector2Int next);
                    if (found && 
                        MyChair == null && 
                        AllBarstools[i].GetMyCustomer() == null && 
                        AllBarstools[i] is EntityBarstool)
                    {
                        MyChair = AllBarstools[i];
                        EntityBarstool chair = MyChair as EntityBarstool;
                        chair.SetMyCustomer(this);
                        break;
                    }
                }

                // Check all grey chairs
                for (int i = 0; i < AllGreyChairs.Length; i++)
                {
                    bool found = Grid.Pathfind(Position, AllGreyChairs[i].Position, IsPassable, out Vector2Int next);
                    if (found &&
                        MyChair == null &&
                        AllGreyChairs[i].GetMyCustomer() == null &&
                        AllGreyChairs[i] is EntityChairGrey)
                    {
                        MyChair = AllGreyChairs[i];
                        EntityChairGrey chair = MyChair as EntityChairGrey;
                        chair.SetMyCustomer(this);
                        break;
                    }
                }

                // Check all red chairs
                for (int i = 0; i < AllRedChairs.Length; i++)
                {
                    bool found = Grid.Pathfind(Position, AllRedChairs[i].Position, IsPassable, out Vector2Int next);
                    if (found &&
                        MyChair == null &&
                        AllRedChairs[i].GetMyCustomer() == null &&
                        AllRedChairs[i] is EntityChairRed)
                    {
                        MyChair = AllRedChairs[i];
                        EntityChairRed chair = MyChair as EntityChairRed;
                        chair.SetMyCustomer(this);
                        break;
                    }
                }

                // Check all rough chairs
                for (int i = 0; i < AllRoughChairs.Length; i++)
                {
                    bool found = Grid.Pathfind(Position, AllRoughChairs[i].Position, IsPassable, out Vector2Int next);
                    if (found &&
                        MyChair == null &&
                        AllRoughChairs[i].GetMyCustomer() == null &&
                        AllRoughChairs[i] is EntityChairRough)
                    {
                        MyChair = AllRoughChairs[i];
                        EntityChairRough chair = MyChair as EntityChairRough;
                        chair.SetMyCustomer(this);
                        break;
                    }
                }

                // Check all smooth chairs
                for (int i = 0; i < AllSmoothChairs.Length; i++)
                {
                    bool found = Grid.Pathfind(Position, AllSmoothChairs[i].Position, IsPassable, out Vector2Int next);
                    if (found &&
                        MyChair == null &&
                        AllSmoothChairs[i].GetMyCustomer() == null &&
                        AllSmoothChairs[i] is EntityChairSmooth)
                    {
                        MyChair = AllSmoothChairs[i];
                        EntityChairSmooth chair = MyChair as EntityChairSmooth;
                        chair.SetMyCustomer(this);
                        break;
                    }
                }
            }
            else if ((Position - MyChair.Position).magnitude < Range)
            {
                // At chair
                CurrentState = State.SitDown;
                UnityEngine.Debug.LogWarning("At chair");
            }
            else 
            {
                // Chair found
                bool found = Grid.Pathfind(Position, MyChair.Position, IsChairPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, Speed);
                }
            }
        }
    }

    private void OnSitDown() 
    {
        if (!IsMoving)
        {
            if (MyChair == null)
            {
                // Find chair
                CurrentState = State.GoingToEmptyChair;
            }
            else if ((Position - MyChair.Position).magnitude < Range)
            {
                // Near chair, sit down
                bool found = Grid.Pathfind(Position, MyChair.Position, IsChairPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, Speed);
                }
            }
            else
            {
                // Neither near chair or at chair
                CurrentState = State.GoingToEmptyChair;
            }
            if (Position == MyChair.Position) 
            { 
                // At chair, wait
                CurrentState = State.WaitAtChair;
            }
        }
    }

    private void OnWaitAtChair()
    {
        if (!IsMoving)
        {
            if (MyCoffee == null)
            {
                // Do nothing, wait for coffee
                EntityCoffee[] AllCoffeeCups = FindObjectsOfType<EntityCoffee>();
                for (int i = 0; i < AllCoffeeCups.Length; i++)
                {
                    if (AllCoffeeCups[i].GetCustomer() == null)
                    {
                        MyCoffee = AllCoffeeCups[i];
                        AllCoffeeCups[i].SetCustomer(this);
                    }
                }
            }
            else 
            {
                CurrentState = State.GoingToDrink;
            }
        }
    }

    private void OnWaitAtRandomLocation()
    {
        // TO-DO: Add random location method
    }

    private void OnGoingToDrink()
    {
        if (!IsMoving)
        {
            if (MyCoffee == null)
            {
                CurrentState = State.WaitAtChair;
            }
            else if ((Position - MyCoffee.Position).magnitude < Range)
            {
                // In range of coffee item, pick it up
                MyCoffee.transform.position = gameObject.transform.position;
                MyCoffee.transform.SetParent(this.transform);
                CurrentState = State.LeavingCafe;
                UnityEngine.Debug.LogWarning("At coffee");
            }
            else
            {
                // Go to coffee found
                bool found = Grid.Pathfind(Position, MyCoffee.Position, IsChairPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, Speed);
                }
            }
        }
    }

    private void OnLeavingCafe()
    {
        if (didReview == false)
        {
            int Opinion = UnityEngine.Random.Range(0, 1);
            if (Opinion == 0)
            {
                GetECO.CurrentReviews = GetECO.CurrentReviews + 1;
                GetECO.NegativeReviews = GetECO.NegativeReviews + 1;
            }
            else if (Opinion == 1)
            {
                GetECO.CurrentReviews = GetECO.CurrentReviews + 1;
                GetECO.NegativeReviews = GetECO.PositiveReviews + 1;

                StoreLevelManager LevelManager = FindObjectOfType<StoreLevelManager>();
                LevelManager.CustomerAddXP();
            }
            didReview = true;
        }
        Destroy(this.gameObject);
    }

    private bool IsPassable(Vector2Int position)
    {
        // Customers can only walk on:
        // Concrete, Foundation, Furnitures
        if (Grid.HasPriority(position, EntityPriority.Foundations) ||
            Grid.HasPriority(position, EntityPriority.Furniture) ||
            Grid.HasEntity<EntityConcrete>(position)
            )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsChairPassable(Vector2Int position) 
    {
        if (Grid.HasPriority(position, EntityPriority.Characters))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override string OnSerialize()
    {
        CustomerVars vars = new CustomerVars();
        vars.MyStateData = CurrentState.ToString();
        vars.MyItemData = MyItem.GetItemName();
        vars.MyCustomerIDData = MyCustomerID;

        return JsonUtility.ToJson(vars);
    }

    public override void OnDeserialize(string json)
    {
        CustomerVars vars = JsonUtility.FromJson<CustomerVars>(OnSerialize());
        switch (vars.MyStateData)
        {
            case "WaitingForCafe":
                CurrentState = State.WaitingForCafe;
                break;
            case "GoingToRegister":
                CurrentState = State.GoingToRegister;
                break;
            case "DisplayText":
                CurrentState = State.DisplayText;
                break;
            case "GoingToEmptyChair":
                CurrentState = State.GoingToEmptyChair;
                break;
            case "SitDown":
                CurrentState = State.SitDown;
                break;
            case "WaitAtChair":
                CurrentState = State.WaitAtChair;
                break;
            case "WaitAtRandomLocation":
                CurrentState = State.WaitAtRandomLocation;
                break;
            case "GoingToDrink":
                CurrentState = State.GoingToDrink;
                break;
            case "LeavingCafe":
                CurrentState = State.LeavingCafe;
                break;
        }
        MenuItem[] AllMenuItems = FindObjectsOfType<MenuItem>();
        for (int i = 0; i < AllMenuItems.Length; i++)
        {
            if (AllMenuItems[i].GetItemName() == vars.MyItemData)
            {
                MyItem = AllMenuItems[i];
                break;
            }
        }
        MyCustomerID = vars.MyCustomerIDData;
    }

}
