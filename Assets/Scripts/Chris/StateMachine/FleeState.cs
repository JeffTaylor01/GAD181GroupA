using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeState : State
{
    public WanderState wanderState;
    public ChaseState chaseState;
    public float itDistanceRun = 5;

    private StateManager stateInfo;
    private GameObject tagger;
    public Material fleeColor;    

    private void Start()
    {
        stateInfo = GetComponentInParent<StateManager>();
    }

    public override State RunCurrentState(NavMeshAgent agent)
    {
        tagger = stateInfo.contestants.tagger;

        if (isIT)
        {
            return chaseState;
        }
        else
        {
            if (checkMaterial())
            {
                GetComponentInParent<MeshRenderer>().material = fleeColor;
            }

            if (!canSeeIT() || stateInfo.ignoreIT)
            {
                return wanderState;
            }
            else
            {
                //Debug.Log("Fleeing");
                if (Vector3.Distance(transform.parent.position, tagger.transform.position) <= itDistanceRun)
                {
                    transform.rotation = Quaternion.LookRotation(transform.position - tagger.transform.position);

                    Vector3 runTo = transform.position + transform.forward * 5;

                    NavMeshHit hit;
                    NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));

                    agent.SetDestination(hit.position);
                }
                return this;
            }
        }            
    }

    private bool canSeeIT()
    {
        if (Vector3.Distance(transform.parent.position, tagger.transform.position) <= itDistanceRun)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool checkMaterial()
    {
        if (GetComponentInParent<MeshRenderer>().material != fleeColor)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
