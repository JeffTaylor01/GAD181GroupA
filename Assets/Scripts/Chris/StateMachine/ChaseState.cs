using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public WanderState wanderState;
    public bool isIT;
    public override State RunCurrentState()
    {
        if (!isIT)
        {
            return wanderState;
        }
        else
        {
            Debug.Log("Chasing");
            return this;
        }        
    }
}
