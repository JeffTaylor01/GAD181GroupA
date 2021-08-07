using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.transform.position = destination.position;
            other.transform.rotation = destination.rotation;

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
        }
    }
}
