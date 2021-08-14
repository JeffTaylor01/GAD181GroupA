using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
                other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                StartCoroutine(NavDisableTimer(other.gameObject.GetComponent<NavMeshAgent>()));
            //    var pc = other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            //    exitVel *= pc.velocity.magnitude;
            //    pc.velocity = exitVel;
            }            
        }
    }

    IEnumerator NavDisableTimer(NavMeshAgent agent)
    {
        yield return null;
        yield return null;
        agent.enabled = true;
    }
}
