using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UseEnvironmentObjectState : State
{

    public ChaseState chaseState;
    private StateManager stateInfo;

    private Vector3 destination;
    private bool reachedObject;
    private float objectCooldown = 5;
    private bool runCooldown = false;
    private float cooldownTimer = 0;

    private void Start()
    {
        stateInfo = GetComponentInParent<StateManager>();
    }

    private void Update()
    {
        if (runCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= objectCooldown)
            {
                reachedObject = false;
                runCooldown = false;
                cooldownTimer = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("EnvironmentObject"))
        {
            reachedObject = true;
            runCooldown = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("EnvironmentObject"))
        {
            reachedObject = true;
            runCooldown = true;
        }
    }

    public override State RunCurrentState(NavMeshAgent agent)
    {
        destination = FindObject();
        if (stateInfo.objectInRange && !reachedObject)
        {
            if (!reachedObject)
            {
                agent.SetDestination(destination);
            }
            return this;
        }
        else
        {
            return chaseState;
        }
    } 

    private Vector3 FindObject()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, 25);
        Vector3 objectDest = objects[0].transform.position;
        if (objects.Length > 0)
        {
            foreach (Collider eObject in objects)
            {
                if (eObject.tag.Equals("EnvironmentObject"))
                {
                    if (Vector3.Distance(transform.position, eObject.gameObject.transform.position) < Vector3.Distance(transform.position, objectDest) && eObject.GetComponent<ItemSpawner>().itemSpawned)
                    {
                        objectDest = eObject.gameObject.transform.position;
                    }
                }
            }
        }        
        Debug.Log(gameObject.name + ": Going to Object");
        return objectDest;
    }
}
