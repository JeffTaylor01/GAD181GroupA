using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpeedBoost : MonoBehaviour
{
    public bool destroyOnEnd;
    public float boost = 25;
    public float boostTime;

    public bool runBoost;
    public float timer;
    private float playerSpeed;
    private GameObject contestant;

    private Collider col;
    private MeshRenderer mesh;

    // Start is called before the first frame update
    private void Start()
    {
        col = GetComponent<Collider>();
        mesh = GetComponent<MeshRenderer>();
        timer = 0;
    }

    private void Update()
    {
        if (runBoost)
        {
            timer += Time.deltaTime;
            Debug.Log("Running Timer");
            if (timer >= boostTime)
            {
                RevokeBoost(contestant);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log(other.gameObject.name + " touched SpeedBoostItem");
            contestant = other.gameObject;
            ApplyBoost(contestant);

            if (destroyOnEnd)
            {
                col.enabled = false;
                mesh.enabled = false;
            }
        }
    }

    private void ApplyBoost(GameObject character)
    {
        bool usePC = false;

        if (character.GetComponent<PlayerCharacterController>() != null)
        {
            usePC = true;
        }

        if (usePC)
        {
            var pc = character.GetComponent<PlayerCharacterController>();
            playerSpeed = pc.speed;
            pc.speed = boost;
        }
        else
        {
            var pc = character.GetComponent<NavMeshAgent>();
            playerSpeed = pc.speed;
            pc.speed = boost;
        }
        

        runBoost = true;
        timer = 0;
    }

    private void RevokeBoost(GameObject character)
    {
        bool usePC = false;

        if (character.GetComponent<PlayerCharacterController>() != null)
        {
            usePC = true;
        }

        if (usePC)
        {
            var pc = character.GetComponent<PlayerCharacterController>();
            pc.speed = playerSpeed;
        }
        else
        {
            var pc = character.GetComponent<NavMeshAgent>();
            pc.speed = playerSpeed;
        }

        runBoost = false;

        if (destroyOnEnd)
        {
            Object.Destroy(gameObject);
        }
    }
}
