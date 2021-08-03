using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpeedBoost : MonoBehaviour
{
    public float boost = 25;
    public float boostTime;

    public bool runBoost;
    public float timer;
    private float playerSpeed;
    public GameObject user;
    private StateManager stateInfo;

    public void UseItem()
    {
        stateInfo = user.GetComponent<StateManager>();
        stateInfo.itemUsed = true;

        bool usePC = false;

        if (user.GetComponent<PlayerCharacterController>() != null)
        {
            usePC = true;
        }

        if (usePC)
        {
            var pc = user.GetComponent<PlayerCharacterController>();
            playerSpeed = pc.speed;
            pc.speed = boost;
        }
        else
        {
            var pc = user.GetComponent<NavMeshAgent>();
            playerSpeed = pc.speed;
            pc.speed = boost;
        }
        

        runBoost = true;
        timer = 0;
    }

    private void RevokeBoost()
    {
        bool usePC = false;

        if (user.GetComponent<PlayerCharacterController>() != null)
        {
            usePC = true;
        }

        if (usePC)
        {
            var pc = user.GetComponent<PlayerCharacterController>();
            pc.speed = playerSpeed;
        }
        else
        {
            var pc = user.GetComponent<NavMeshAgent>();
            pc.speed = playerSpeed;
        }

        runBoost = false;
        stateInfo.itemUsed = false;
        stateInfo.heldItem = null;

        Object.Destroy(gameObject);
    }

    public void RunTimer()
    {
        if (runBoost)
        {
            timer += Time.deltaTime;
            Debug.Log("Running Timer");
            if (timer >= boostTime)
            {
                RevokeBoost();
            }
        }
    }
}
