using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public class EntityCustomer : EntityBase
{
    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Characters/Character002"));
        SetEntityPriority(EntityPriority.Characters);
        SetEntityName("Customer");
    }

    public enum State 
    { 
        GoingToRegister, DisplayText, GoingToEmptyChair, SitDown, WaitAtChair, WaitAtRandomLocation, GoingToDrink, LeavingCafe
    }

    [SerializeField] private State CurrentState = State.GoingToRegister;

    private bool OrderedDrink;

    [SerializeField] private EntityCoffee MyCoffee;
    EntityBase MyChair;
    EntityRegister Register;
    EntityRegister[] AllRegisters;
    List<Sprite> AllOrderingTexts = new List<Sprite>();
    GameObject MyTextBubble;
    Stopwatch TextWatch = new Stopwatch();

    private void Awake()
    {
        UnityEngine.Object[] LoadOrderingTexts = Resources.LoadAll("Sprites/UI/TextBubbles/Ordered");
        for (int i = 0; i < LoadOrderingTexts.Length; i++)
        {
            AllOrderingTexts.Add(LoadOrderingTexts[i] as Sprite);
        }
    }

    private void FixedUpdate()
    {
        AllRegisters = FindObjectsOfType<EntityRegister>();
        switch (CurrentState)
        {
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

    private void OnGoingToRegister()
    {
        if (!IsMoving)
        {

            for (int i = 0; i < AllRegisters.Length; i++)
            {
                if (AllRegisters[i].GetCustomer() == null)
                {
                    Register = AllRegisters[i];
                    Register.SetCustomer(this);
                    break;
                }
            }

            if (Register == null)
            {
                // Do nothing, wait for register
            }
            else if ((Position - Register.Position).magnitude < 2.5f)
            {
                CurrentState = State.DisplayText;
                UnityEngine.Debug.LogWarning("At register");
            }
            else
            {
                bool found = Grid.Pathfind(Position, Register.Position, IsPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, 0.25f);
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
            textBubble.transform.position = new Vector3(transform.position.x, transform.position.y + 5, -10);
            textBubble.transform.SetParent(this.transform);

            // get random customer text
            int index = UnityEngine.Random.Range(0, (AllOrderingTexts.Count - 1));

            textBubble.transform.GetChild(0).GetComponent<Image>().sprite = AllOrderingTexts[index];

        }
        else
        {
            if (TextWatch.Elapsed >= new TimeSpan(0, 0, 2))
            {
                OrderedDrink = true;

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
            else if ((Position - MyChair.Position).magnitude < 2.0f)
            {
                // At chair
                CurrentState = State.SitDown;
                UnityEngine.Debug.LogWarning("At chair");
            }
            else 
            {
                // Chair found
                bool found = Grid.Pathfind(Position, MyChair.Position, IsPassable, out Vector2Int next);
                if (found)
                {
                    Move(next, 0.25f);
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
            else if ((Position - MyChair.Position).magnitude < 1.5f)
            {
                // Near chair, sit down
                Move(MyChair.Position, 0.25f);
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
        if (MyCoffee == null)
        {
            CurrentState = State.WaitAtChair;
        }
        else if ((Position - MyCoffee.Position).magnitude < 1.5f)
        {
            MyCoffee.transform.position = gameObject.transform.position;
            MyCoffee.transform.SetParent(this.transform);
            CurrentState = State.LeavingCafe;
            UnityEngine.Debug.LogWarning("At coffee");
        }
        else
        {
            // Go to coffee found
            bool found = Grid.Pathfind(Position, MyCoffee.Position, IsPassable, out Vector2Int next);
            if (found)
            {
                Move(next, 0.25f);
            }
        }
    }

    private void OnLeavingCafe()
    {
        Destroy(this.gameObject);
    }

    private bool IsPassable(Vector2Int position)
    {
        if (Grid.HasPriority(position, EntityPriority.Furniture) ||
            Grid.HasPriority(position, EntityPriority.Characters))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
