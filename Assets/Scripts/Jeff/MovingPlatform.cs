using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> waypoints;
    public float moveSpeed = 2;
    public int target;
    public float maxDistance = 0;
    public float distance;

    private bool stop = false;
    public float stopTime = 2;
    private float timer;

    private void Start()
    {
        stop = false;
        if (waypoints.Count == 2)
        {
            maxDistance = Vector3.Distance(waypoints[0].position, waypoints[1].position);
        }
    }

    private void Update()
    {
        if (stop)
        {
            timer += Time.deltaTime;
            if (timer >= stopTime)
            {
                stop = false;
                timer = 0;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (!stop)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[target].position, moveSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, waypoints[0].position);
        }        

        if (transform.position == waypoints[target].position)
        {
            if (target == waypoints.Count - 1)
            {
                target = 0;
            }
            else
            {
                target += 1;
            }
            stop = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerCharacterController>() != null)
            {
                collision.gameObject.transform.parent = gameObject.transform;
            }            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerCharacterController>() != null)
            {
                collision.gameObject.transform.parent = null;
            }            
        }
    }

}
