using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{

    public ContestantManager contestants;
    public bool isIT;
    public bool isPlayer;

    public Color playerColor;
    public Color wanderColor;
    public Color itColor;

    public State currentState;
    public NavMeshAgent agent;
    public Rigidbody rb;

    private float taggedCooldown = 2.5f;
    private float cooldownTimer;
    private bool runTagCooldown;
    public bool canTag;
    public float agentSpeed;

    public bool ignoreIT;
    private float ignoreTime = 2;
    private float igTimer;

    public bool shielded;
    public GameObject heldItem;
    public bool itemInRange;
    public bool itemUsed;
    public bool objectInRange;
    public bool stunned;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        if (contestants == null)
        {
            contestants = GameObject.FindGameObjectWithTag("GameController").GetComponent<ContestantManager>();
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (isIT)
        {
            currentState.isIT = true;
            canTag = true;
        }        
        else
        {
            agent.speed = agentSpeed;
            currentState.isIT = false;
            canTag = false;
        }

        checkMaterial();

        if (runTagCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= taggedCooldown)
            {
                agent.speed = agentSpeed;
                runTagCooldown = false;
                canTag = true;
            }
        }
        else
        {
            if (ignoreIT)
            {
                igTimer += Time.deltaTime;
                if (igTimer >= ignoreTime)
                {
                    ignoreIT = false;
                }
            }            
            FindItem();
            FindObject();
            RunStateMachine();
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Surface") && agent.enabled != true && !isPlayer)
        {
            agent.enabled = true;
            rb.isKinematic = true;
        }
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState(agent);

        if (nextState != null)
        {
            //Switch to Next State
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }

    private void FindItem()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("ItemSpawner");
        itemInRange = false;
        foreach (GameObject spawner in spawners)
        {
            if (Vector3.Distance(transform.position, spawner.gameObject.transform.position) <= 25 && spawner.GetComponent<ItemSpawner>().itemSpawned)
            {
                itemInRange = true;
            }
        }
    }

    private void FindObject()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("EnvironmentObject");
        objectInRange = false;
        foreach (GameObject eObject in objects)
        {
            if (Vector3.Distance(transform.position, eObject.gameObject.transform.position) <= 25)
            {
                objectInRange = true;
            }
        }
    }

    public void gotTagged(StateManager other)
    {
        if (!shielded)
        {
            Debug.Log(gameObject.name + " gotTagged");
            isIT = true;
            if (gameObject.name == "C_Player")
            {
                contestants.ow.Play();
            }
            runTagCooldown = true;
            agent.speed = 0;
            cooldownTimer = 0;
            canTag = false;
        }        
    }

    public void taggedAnother(StateManager other)
    {
        if (!other.shielded)
        {
            Debug.Log(gameObject.name + " taggedAnother");
            isIT = false;
            ignoreIT = true;
            if (gameObject.name == "C_Player")
            {
                contestants.tag.Play();
            }            
            contestants.ResetTimer();
        }        
    }

    private void checkMaterial()
    {
        if (isIT)
        {
            if (GetComponentInParent<MeshRenderer>().material.color != itColor)
            {
                GetComponentInParent<MeshRenderer>().material.color = itColor;
            }
        }
        else
        {
            if (isPlayer)
            {
                if (GetComponentInParent<MeshRenderer>().material.color != playerColor)
                {
                    GetComponentInParent<MeshRenderer>().material.color = playerColor;
                }
            }
            else
            {
                if (GetComponentInParent<MeshRenderer>().material.color != wanderColor)
                {
                    GetComponentInParent<MeshRenderer>().material.color = wanderColor;
                }
            }            
        }
    }
}
