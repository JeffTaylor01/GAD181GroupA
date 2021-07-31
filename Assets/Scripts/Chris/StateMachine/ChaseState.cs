using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    public WanderState wanderState;
    public FindItemState itemState;

    public GameObject[] targets;
    public GameObject target;

    private StateManager stateInfo;
    private GameObject tagger;
    public Material taggerColor;
    private bool gotTargets;

    public float distance;

    private void Start()
    {
        stateInfo = GetComponentInParent<StateManager>();
    }
    public override State RunCurrentState(NavMeshAgent agent)
    {
        tagger = stateInfo.contestants.tagger;
        if (stateInfo.heldItem == null && stateInfo.itemInRange)
        {
            return itemState;
        }
        if (isIT)
        {
            //Debug.Log("Chasing");
            if (checkMaterial())
            {
                GetComponentInParent<MeshRenderer>().material = taggerColor;
            }
            
            //isIT = true;
            if (!gotTargets)
            {
                targets = getTargets();
            }

            findClosest();
            if ((Vector3.Distance(gameObject.transform.parent.position, target.transform.position) <= 1.1) && stateInfo.canTag)
            {
                Debug.Log(gameObject.transform.parent.name + " tagged: " + target.name);
                if (!target.GetComponent<StateManager>().shielded)
                {
                    tagged();
                }
            }
            
            agent.SetDestination(target.transform.position);
            distance = Vector3.Distance(gameObject.transform.parent.position, target.transform.position);
            return this;
        }
        else
        {
            gotTargets = false;
            return wanderState;
        }     
    }

    private GameObject[] getTargets()
    {
        GameObject[] pseudos = stateInfo.contestants.contestants;
        var targs = new GameObject[pseudos.Length - 1];
        int targetIndex = 0;
        for (int i = 0; i < pseudos.Length; i++)
        {
            if (pseudos[i] != gameObject.transform.parent.gameObject)
            {
                targs[targetIndex] = pseudos[i];
                targetIndex++;
            }
        }
        gotTargets = true;
        return targs;
    }

    private void findClosest()
    {
        target = targets[0];
        
        for (int i = 1; i < targets.Length; i++)
        {
            var dist = Vector3.Distance(transform.parent.position, target.transform.position);
            if (Vector3.Distance(transform.parent.position, targets[i].transform.position) < dist)
            {
                target = targets[i];
            }
        }
    }

    private void tagged()
    {
        stateInfo.taggedAnother();
        target.GetComponent<StateManager>().gotTagged();
        stateInfo.contestants.tagger = target;
    }

    private bool checkMaterial()
    {
        if (GetComponentInParent<MeshRenderer>().material != taggerColor)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
