using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeState : State
{
    public WanderState wanderState;
    public ChaseState chaseState;
    public float itDistanceRun = 5;
    public UseEnvironmentObjectState objectState;

    private StateManager stateInfo;
    private GameObject tagger;
    public Material fleeColor;

    public float itemUseChance;
    private float chanceTimer = 0;

    private void Start()
    {
        stateInfo = GetComponentInParent<StateManager>();
    }

    public override State RunCurrentState(NavMeshAgent agent)
    {
        runChance();
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

            if (stateInfo.heldItem != null)
            {
                bool useItem = false;

                if (stateInfo.heldItem.tag.Equals("SpeedBoostItem"))
                {
                    if (itemUseChance > 0.2)
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
                    if (itemUseChance > 0.4)
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

    private void runChance()
    {
        if (chanceTimer >= 3 && stateInfo.heldItem != null)
        {
            itemUseChance = Random.value;
            chanceTimer = 0;
        }

        if (stateInfo.heldItem != null)
        {
            chanceTimer += Time.deltaTime;
            Debug.Log(chanceTimer);
        }
    }
}
