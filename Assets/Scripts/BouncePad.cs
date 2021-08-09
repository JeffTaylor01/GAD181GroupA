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
            BouncePlayer(collision.gameObject);
        }        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            BouncePlayer(collision.gameObject);
        }
    }

    void BouncePlayer(GameObject player)
    {
        bool usePC = false;

        Vector3 bounce = gameObject.transform.up * bounceAmount;

        if (player.GetComponent<PlayerCharacterController>() != null)
        {
            usePC = true;
        }

        if (usePC)
        {
            var pc = player.GetComponent<Rigidbody>();
            pc.AddForce(bounce, ForceMode.VelocityChange);
        }
        else
        {
            var pc = player.GetComponent<Rigidbody>();
            pc.AddForce(bounce, ForceMode.VelocityChange);
        }
    }
}
