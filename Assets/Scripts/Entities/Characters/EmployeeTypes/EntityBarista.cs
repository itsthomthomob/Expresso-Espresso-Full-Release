using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBarista : EntityBase
{
    private int EmployeeID;
    private string EmployeeName;
    private string SpriteName;
    private float WageAmount;
    private float SkillModifier;
    private float EfficiencyModifier;
    private string PersonalityType;

    public void SetWageAmount(float wageOffer) { WageAmount = wageOffer; }
    public float GetWageAmount(){ return WageAmount; }

    public void SetEmployeeID(int newID) { EmployeeID = newID; }
    public int GetEmployeeID() { return EmployeeID; }

    public void SetEmployeePersonality(string newPers) { PersonalityType = newPers; }
    public string GetEmployeePersonality() { return PersonalityType; }
    public void SetSpriteName(string newSprite) { SpriteName = newSprite; }
    public string GetSpriteName() { return SpriteName; }
    public void SetEmployeeName(string newName) { EmployeeName = newName; }
    public string GetEmployeeName() { return EmployeeName; }
    public void SetSkillModifier(float newSkill) { SkillModifier = newSkill; }
    public float GetSkillModifier() { return SkillModifier; }
    public void SetEfficiencyModifier(float newEff) { EfficiencyModifier = newEff; }
    public float GetEfficiencyModifier() { return EfficiencyModifier; }

    public override void OnEntityAwake()
    {
        SetEntitySprite(Resources.Load<Sprite>("Sprites/Characters/Character001"));
        SetEntityPriority(EntityPriority.Characters);
        SetEntityName("Barista");
    }
}
