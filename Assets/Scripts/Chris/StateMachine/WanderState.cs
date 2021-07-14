using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{
    public FleeState fleeState;
    public bool canSeeIT;

    public override State RunCurrentState()
    {
        if (canSeeIT)
        {
            return fleeState;
        }
        else
        {
            Debug.Log("Wandering");
            return this;
        }        
    }
}
