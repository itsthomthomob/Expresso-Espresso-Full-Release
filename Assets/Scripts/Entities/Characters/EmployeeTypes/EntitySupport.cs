using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySupport : EntityBase
{

    public enum State 
    {
        TravelToRoaster, FillRoaster, TravelToBrewer, FillBrewer, TravelToEspresso, FillEspresso
    }

    public State CurrentState = State.TravelToRoaster;

    public override string GetEntityName()
    {
        return "Support";
    }

    protected override EntityPriority GetEntityPriority()
    {
        return EntityPriority.Characters;
    }

    protected override Sprite GetEntitySprite()
    {
        return Resources.Load<Sprite>("Sprites/Tiles/Sprites/Characters/Character001");
    }

    private void FixedUpdate()
    {
        switch (CurrentState)
        {
            case State.TravelToRoaster:
                OnTravelToRoaster();
                break;
            case State.FillRoaster:
                OnFillRoaster();
                break;
            case State.TravelToBrewer:
                OnTravelToBrewer();
                break;
            case State.FillBrewer:
                OnFillBrewer();
                break;
            case State.TravelToEspresso:
                OnTravelToEspresso();
                break;
            case State.FillEspresso:
                OnFillEspresso();
                break;
            default:
                break;
        }
    }

    private void OnTravelToRoaster()
    {
        throw new NotImplementedException();
    }

    private void OnFillRoaster()
    {
        throw new NotImplementedException();
    }

    private void OnTravelToBrewer()
    {
        throw new NotImplementedException();
    }

    private void OnFillBrewer()
    {
        throw new NotImplementedException();
    }

    private void OnTravelToEspresso()
    {
        throw new NotImplementedException();
    }

    private void OnFillEspresso()
    {
        throw new NotImplementedException();
    }
}
