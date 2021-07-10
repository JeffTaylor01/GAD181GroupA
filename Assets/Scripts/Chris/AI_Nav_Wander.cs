using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Nav_Wander : MonoBehaviour
{

    public float xMax = 10;
    public float xMin = 10;
    public float zMax = 10;
    public float zMin = 10;

    private NavMeshAgent agent;
    private Vector3 destination;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        newWayPoint();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 3)
        {
            newWayPoint();
        }
    }

    private void newWayPoint()
    {
        Vector3 point = new Vector3(Random.Range(xMin, xMax), transform.position.y, Random.Range(zMin, zMax));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(point, out hit, 100f, NavMesh.GetAreaFromName("Walkable")))
        {
            destination = hit.position;
            agent.SetDestination(destination);
        }
        else
        {
            destination = point;
            agent.SetDestination(destination);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(destination, 1f);
    }
}