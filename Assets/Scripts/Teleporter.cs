using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;

    private void Start()
    {
        destination = transform.GetChild(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {            
            bool usePC = false;

            Vector3 exitVel = destination.forward;

            if (other.gameObject.GetComponent<PlayerCharacterController>() != null)
            {
                usePC = true;
            }

            if (usePC)
            {
                var pc = other.gameObject.GetComponent<Rigidbody>();
                exitVel *= pc.velocity.magnitude;
                pc.velocity = exitVel;
            }
            else
            {
                var pc = other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
                exitVel *= pc.velocity.magnitude;
                pc.velocity = exitVel;
            }
            other.gameObject.transform.position = destination.position;
            other.gameObject.transform.rotation = destination.rotation;
        }
    }
}
