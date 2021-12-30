using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEmployee : EntityBase
{

	private World World;
	private Vector2Int Destination;
	private float Speed;
	private float Wage;
	private float EfficiencyModifier;
	private float SkillModifier;

	private void Awake()
	{
		World = FindObjectOfType<World>();
		Destination = World.GetRandomLocation();
		Speed = Random.Range(0.1f, 0.5f);
	}

	protected override Sprite GetEntitySprite()
	{
		return null;
	}

	protected override EntityPriority GetEntityPriority()
	{
		return EntityPriority.Characters;
	}
}
