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
        if (stateInfo.heldItem == null)
        {
            stateInfo.itemUsed = false;
        }
        if (stateInfo.heldItem != null)
        {
            bool useItem = false;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player Pressed E");
                useItem = true;
            }

            if (stateInfo.heldItem.tag.Equals("SpeedBoostItem"))
            {
                var item = stateInfo.heldItem.GetComponent<SpeedBoost>();
                item.user = transform.parent.gameObject;

                if (useItem && !stateInfo.itemUsed)
                {
                    item.UseItem();
                }
                item.RunTimer();
            }
            else if (stateInfo.heldItem.tag.Equals("ShieldItem"))
            {
                var item = stateInfo.heldItem.GetComponent<Shield>();
                item.user = transform.parent.gameObject;

                if (useItem && !stateInfo.itemUsed)
                {
                    item.UseItem();
                }
                item.RunTimer();
            }
            else if (stateInfo.heldItem.tag.Equals("StunItem"))
            {
                var item = stateInfo.heldItem.GetComponent<Stun>();
                item.user = transform.parent.gameObject;
                item.startPos = stateInfo.gameObject.transform.position;

                if (Input.GetMouseButton(1))
                {
                    Debug.Log("Player Aiming");
                    item.aiming = true;
                }
                else
                {
                    item.aiming = false;
                }

                item.Aim();

                if (useItem && !stateInfo.itemUsed && Input.GetMouseButton(1))
                {
                    item.UseItem();
                }
                item.RunTimer();
            }
        }


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
            if ((Vector3.Distance(gameObject.transform.parent.position, target.transform.position) <= 1.1) && stateInfo.canTag)
            {
                Debug.Log(gameObject.transform.parent.name + " tagged: " + target.name);
                if (!target.GetComponent<StateManager>().shielded)
                {
                    tagged();
                }
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