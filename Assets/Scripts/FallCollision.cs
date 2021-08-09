using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollision : MonoBehaviour
{

    public bool destroyPlayer;
    public Transform respawnPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyPlayer)
        {
            Destroy(collision.gameObject);
        }
        else
        {
            collision.transform.position = respawnPoint.position;
        }
    }
}
