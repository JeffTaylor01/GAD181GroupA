using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public GameObject player;
    void OnTriggerEnter(Collider other)
    {
        player.transform.position = destination.transform.position;
    }
}
