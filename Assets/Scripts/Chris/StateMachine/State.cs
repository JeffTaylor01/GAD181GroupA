using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{
    public bool isIT;
    public abstract State RunCurrentState(NavMeshAgent agent);
}
