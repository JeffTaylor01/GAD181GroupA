using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindItemState : State
{

    public ChaseState chaseState;
    private StateManager stateInfo;

    private Vector3 destination;

    private void Start()
    {
        stateInfo = GetComponentInParent<StateManager>();
    }

    public override State RunCurrentState(NavMeshAgent agent)
    {
        destination = FindItem();
        if (stateInfo.heldItem == null)
        {
            agent.SetDestination(destination);
            return this;
        }
        else
        {
            return chaseState;
        }
    }

    private Vector3 FindItem()
    {
        Collider[] spawners = Physics.OverlapSphere(transform.position, 25);
        Vector3 itemDest = spawners[0].transform.position;
        if (spawners.Length > 0)
        {
            foreach (Collider spawner in spawners)
            {
                if (spawner.tag.Equals("ItemSpawner"))
                {
                    if (Vector3.Distance(transform.position, spawner.gameObject.transform.position) < Vector3.Distance(transform.position, itemDest) && spawner.GetComponent<ItemSpawner>().itemSpawned)
                    {
                        itemDest = spawner.gameObject.transform.position;
                    }
                }
            }
        }        
        Debug.Log(gameObject.name + ": Going to item");
        return itemDest;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 25);
        Gizmos.DrawSphere(destination, 1);
    }
}
