using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BouncePad : MonoBehaviour
{

    public float bounceAmount = 10;
    public Transform targetDirection;

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

        Vector3 bounce = (targetDirection.position - transform.position).normalized * bounceAmount;

        if (player.gameObject.GetComponent<PlayerCharacterController>() != null)
        {
            usePC = true;
        }

        if (usePC)
        {
            var pc = player.gameObject.GetComponent<Rigidbody>();
            pc.velocity = bounce;
        }
        else
        {
            var pc = player.gameObject.GetComponent<NavMeshAgent>();
            pc.enabled = false;
            var pa = player.gameObject.GetComponent<Rigidbody>();
            pa.isKinematic = false;
            pa.velocity = bounce;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, targetDirection.position);
    }
}
