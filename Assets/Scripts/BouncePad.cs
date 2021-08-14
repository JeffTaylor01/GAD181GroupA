using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BouncePad : MonoBehaviour
{

    public float bounceAmount = 10;

    //private void OnTriggerEnter(Collider collision)
    //{
    //    Debug.Log("AI on bouncepad");
    //    if (collision.gameObject.tag.Equals("Player"))
    //    {
    //        BouncePlayer(collision);
    //    }        
    //}

    //private void OnTriggerStay(Collider collision)
    //{
    //    //if (collision.gameObject.tag.Equals("Player"))
    //    //{
    //        BouncePlayer(collision);
    //    //}
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("AI on bouncepad");
        if (collision.gameObject.tag.Equals("Player"))
        {
            BouncePlayer(collision.collider);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            BouncePlayer(collision.collider);
        }
    }

    void BouncePlayer(Collider player)
    {
        bool usePC = false;

        

        Vector3 bounce = gameObject.transform.up * bounceAmount;

        if (player.gameObject.GetComponent<PlayerCharacterController>() != null)
        {
            usePC = true;
        }

        if (usePC)
        {
            var pc = player.gameObject.GetComponent<Rigidbody>();
            pc.AddForce(bounce, ForceMode.VelocityChange);
        }
        else
        {
            var pc = player.gameObject.GetComponent<NavMeshAgent>();
            pc.enabled = false;
            var pa = player.gameObject.GetComponent<Rigidbody>();
            pa.AddForce(bounce, ForceMode.VelocityChange);
        }
    }
}
