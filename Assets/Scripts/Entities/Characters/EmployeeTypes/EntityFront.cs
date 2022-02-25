using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFront : EntityBase
{
    private int EmployeeID;
    private string EmployeeName;
    private string SpriteName;
    private float WageAmount;
    private float SkillModifier;
    private string PersonalityType = "N/A";

    private float EfficiencyModifier;
    public string GetEmployeeRole() { return "Front"; }

    public void SetEmployeeID(int newID) { EmployeeID = newID; }
    public int GetEmployeeID() { return EmployeeID; }

    public void SetSpriteName(string newSprite) { SpriteName = newSprite; }
    public string GetSpriteName() { return SpriteName; }
    public void SetEmployeeName(string newName) { EmployeeName = newName; }
    public string GetEmployeeName() { return EmployeeName; }
    public void SetSkillModifier(float newSkill) { SkillModifier = newSkill; }
    public float GetSkillModifier() { return SkillModifier; }
    public void SetEfficiencyModifier(float newEff) { EfficiencyModifier = newEff; }
    public float GetEfficiencyModifier() { return EfficiencyModifier; }
    public string GetEmployeePersonality() { return PersonalityType; }
    public void SetEmployeePersonality(string newPers) { PersonalityType = newPers; }

    public float GetWageAmount() { return WageAmount; }
    public void SetWageAmount(float wageOffer) { WageAmount = wageOffer; }


    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Characters/Character001"));
        SetEntityPriority(EntityPriority.Characters);
        SetEntityName("Front");
    }
}
