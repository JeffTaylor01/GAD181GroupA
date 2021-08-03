using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    public WanderState wanderState;
    public FindItemState itemState;
    //public UseEnvironmentObjectState objectState;

    public GameObject[] targets;
    public GameObject target;

    private StateManager stateInfo;
    private GameObject tagger;
    public Material taggerColor;
    private bool gotTargets;

    public float distance;

    public float itemUseChance;
    private float itemCTimer = 0;    

    private void Start()
    {
        stateInfo = GetComponentInParent<StateManager>();
    }
    public override State RunCurrentState(NavMeshAgent agent)
    {
        runChance();
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

            ItemLogic();

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

    private void ItemLogic()
    {
        if (stateInfo.heldItem != null)
        {
            bool useItem = false;

            if (stateInfo.heldItem.tag.Equals("SpeedBoostItem"))
            {
                if (itemUseChance > 0.4)
                {
                    Debug.Log("Pseudo Used Item");
                    useItem = true;
                }
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
                if (itemUseChance > 0.80)
                {
                    Debug.Log("Pseudo Used Item");
                    useItem = true;
                }
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
                if (itemUseChance > 0.5)
                {
                    Debug.Log("Pseudo Used Item");
                    useItem = true;
                }
                var item = stateInfo.heldItem.GetComponent<Stun>();
                item.user = transform.parent.gameObject;
                item.startPos = stateInfo.gameObject.transform.position;

                if (useItem && !stateInfo.itemUsed)
                {
                    item.aiming = true;
                    item.Aim();
                    item.UseItem();
                }
                item.RunTimer();
            }
        }
        else
        {
            itemUseChance = 0;
        }
    }

    private void runChance()
    {
        if (itemCTimer >= 3 && stateInfo.heldItem != null)
        {
            itemUseChance = Random.value;
            itemCTimer = 0;
        }

        if (stateInfo.heldItem != null)
        {
            itemCTimer += Time.deltaTime;
        }
    }
}
