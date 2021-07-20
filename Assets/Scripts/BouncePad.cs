using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BouncePad : MonoBehaviour
{

    public float bounceAmount = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            bool usePC = false;

            Vector3 bounce = gameObject.transform.up * bounceAmount;

            if (collision.gameObject.GetComponent<PlayerCharacterController>() != null)
            {
                usePC = true;
            }

            if (usePC)
            {
                var pc = collision.gameObject.GetComponent<Rigidbody>();
                pc.velocity = bounce;
            }
            else
            {
                var pc = collision.gameObject.GetComponent<NavMeshAgent>();
                pc.velocity = bounce;
            }            
        }
    }
}
