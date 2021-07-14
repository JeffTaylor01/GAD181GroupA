using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State
{
    public WanderState wanderState;
    public bool canSeeIT;
    public override State RunCurrentState()
    {
        if (!canSeeIT)
        {
            return wanderState;
        }
        else
        {
            Debug.Log("Fleeing");
            return this;
        }        
    }
}
