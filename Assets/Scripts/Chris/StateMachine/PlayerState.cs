using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerState : State
{
    private StateManager stateInfo;
    private GameObject tagger;
    public Material playerColor;
    public Material taggerColor;

    private bool gotTargets;
    public GameObject[] targets;
    public GameObject target;
    public float distance;

    private void Start()
    {
        stateInfo = GetComponentInParent<StateManager>();
    }

    public override State RunCurrentState(NavMeshAgent agent)
    {
        tagger = stateInfo.contestants.tagger;

        if (isIT)
        {
            Debug.Log("Player is IT");
            GetComponentInParent<MeshRenderer>().material = taggerColor;

            if (!gotTargets)
            {
                targets = getTargets();
            }

            findClosest();
            if ((Vector3.Distance(gameObject.transform.parent.position, target.transform.position) <= 1) && stateInfo.canTag)
            {
                Debug.Log(gameObject.transform.parent.name + " tagged: " + target.name);
                tagged();
            }

            distance = Vector3.Distance(gameObject.transform.parent.position, target.transform.position);
        }
        else if (checkMaterial())
        {
            GetComponentInParent<MeshRenderer>().material = playerColor;
        }
        
        return this;
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
        if (GetComponentInParent<MeshRenderer>().material != playerColor)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
