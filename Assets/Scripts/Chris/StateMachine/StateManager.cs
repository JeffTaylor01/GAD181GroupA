using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{

    public ContestantManager contestants;
    public bool isIT;

    public State currentState;
    private NavMeshAgent agent;

    private float taggedCooldown = 2;
    private float cooldownTimer;
    private bool runTagCooldown;
    public bool canTag;
    private float agentSpeed;

    public bool ignoreIT;
    private float ignoreTime = 2;
    private float igTimer;

    public bool shielded;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
    }
    // Update is called once per frame
    private void Update()
    {
        if (runTagCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= taggedCooldown)
            {
                runTagCooldown = false;
                canTag = true;
                agent.speed = agentSpeed;
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
            if (contestants == null)
            {
                contestants = GameObject.FindGameObjectWithTag("GameController").GetComponent<ContestantManager>();
            }
            if (isIT)
            {
                currentState.isIT = true;
                canTag = true;
            }
            else
            {
                currentState.isIT = false;
                canTag = false;
            }
            RunStateMachine();
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

    public void gotTagged()
    {
        if (!shielded)
        {
            Debug.Log(gameObject.name + " gotTagged");
            isIT = true;
            runTagCooldown = true;
            agent.speed = 0;
            cooldownTimer = 0;
            canTag = false;
        }        
    }

    public void taggedAnother()
    {
        if (!shielded)
        {
            Debug.Log(gameObject.name + " taggedAnother");
            isIT = false;
            ignoreIT = true;
        }        
    }
}
