using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityEmployee : EntityBase
{
	private World World;
	private Vector2Int Destination;
	private float Speed;

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

	public void SetSpriteName(string newSprite) { SpriteName = newSprite; }
	public void SetEmployeeName(string newName) { EmployeeName = newName; }
	public string GetEmployeeName(string newName) { return EmployeeName; }
	public void SetWageAmount(float newWage) { WageAmount = newWage; }
	public float GetWageAmount() { return WageAmount;}
	public void SetDays(Image[] newDays) { days = newDays; }
	public Image[] GetDays() { return days; }
	public void SetSkillModifer(float newSkill) { SkillModifier = newSkill;}
	public float GetSkillModifer() { return SkillModifier; }
	public void SetEfficiencyModifer(float newEff) { EfficiencyModifier = newEff; }
	public float GetEfficiencyModifer(float newEff) { return EfficiencyModifier; }

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
