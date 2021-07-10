using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Nav_Flee : MonoBehaviour
{
    public GameObject it;
    public float itDistanceRun = 5;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, it.transform.position) <= itDistanceRun)
        {
            Vector3 itDir = transform.position - it.transform.position;
            Vector3 newPos = transform.position + itDir;

            agent.SetDestination(newPos);
        }
    }
}
