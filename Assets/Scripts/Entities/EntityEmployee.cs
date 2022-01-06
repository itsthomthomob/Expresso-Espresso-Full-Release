using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityEmployee : EntityBase
{
	public enum EmployeeRoles
	{
		Barista,
		Support,
		Front
	}
	public enum PersonalityTypes
	{
		Introvert,
		Extrovert
	}

	private World World;
	private Vector2Int Destination;
	private float Speed;

	private int EmployeeID;
	private EmployeeRoles EmployeeRole;
	private PersonalityTypes EmployeePersonality;
	private string EmployeeName;
	private string SpriteName;
	private float WageAmount;
	private float SkillModifier;
	private float EfficiencyModifier;
	private Image[] days;

	private void Awake()
	{
		World = FindObjectOfType<World>();
		Destination = World.GetRandomLocation();
		Speed = Random.Range(0.1f, 0.5f);
		SpriteName = "Character001";
	}

	public void SetEmployeeID(int newID) { EmployeeID = newID; }
	public int GetEmployeeID() { return EmployeeID; }

	public void SetSpriteName(string newSprite) { SpriteName = newSprite; }
	public string GetSpriteName() { return SpriteName; }
	public void SetEmployeeName(string newName) { EmployeeName = newName; }
	public string GetEmployeeName() { return EmployeeName; }
	public void SetEmployeeRole(EmployeeRoles newRole) { EmployeeRole = newRole; }
	public EmployeeRoles GetEmployeeRole() { return EmployeeRole; }
	public void SetEmployeePersonality(PersonalityTypes newPers) { EmployeePersonality = newPers; }
	public PersonalityTypes GetEmployeePersonality() { return EmployeePersonality; }
	public void SetWageAmount(float newWage) { WageAmount = newWage; }
	public float GetWageAmount() { return WageAmount;}
	public void SetDays(Image[] newDays) { days = newDays; }
	public Image[] GetDays() { return days; }
	public void SetSkillModifier(float newSkill) { SkillModifier = newSkill;}
	public float GetSkillModifier() { return SkillModifier; }
	public void SetEfficiencyModifier(float newEff) { EfficiencyModifier = newEff; }
	public float GetEfficiencyModifier() { return EfficiencyModifier; }

	protected override Sprite GetEntitySprite()
	{
		return Resources.Load<Sprite>("Sprites/Characters/" + SpriteName);
	}

	protected override EntityPriority GetEntityPriority()
	{
		return EntityPriority.Characters;
	}

	// Destination
	private void Update()
	{
		if (!IsMoving)
		{
			if (Position == Destination) Destination = World.GetRandomLocation();
			Vector2Int next = Grid.Pathfind(Position, Destination, IsPassableForAlien);
			if (next == Position) Destination = World.GetRandomLocation();
			//Grid.Move(this, next, Speed);
		}
	}

	private bool IsPassableForAlien(Vector2Int position)
	{
		if (Grid.HasEntity<EntityWall>(position))
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}
