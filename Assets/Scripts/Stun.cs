using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stun : MonoBehaviour
{

    public float stunTime;

    public bool runStun;
    public bool aiming;
    public bool thrown;
    public float timer;
    public GameObject projectile;
    public GameObject user;
    private StateManager stateInfo;
    private Camera camera;

    public Transform targetPos;
    public Vector3 startPos;
    public float stunRadius = 8;
    public float speed = 10;
    public float arcHeight = 1;
    public float aimRange = 25;

    private Collider[] collisions;

    public void UseItem()
    {
        stateInfo = user.GetComponent<StateManager>();
        stateInfo.itemUsed = true;
        thrown = true;

        timer = 0;
    }

    private void RemoveStun()
    {
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].gameObject.tag.Equals("Player"))
            {
                bool usePC = false;

                if (collisions[i].GetComponent<PlayerCharacterController>() != null)
                {
                    usePC = true;
                }

                if (usePC)
                {
                    var pc = collisions[i].GetComponent<PlayerCharacterController>();
                    pc.speed = 10;
                }
                else
                {
                    var pc = collisions[i].GetComponent<NavMeshAgent>();
                    pc.speed = 25;
                }
            }
        }

        runStun = false;
        stateInfo.heldItem = null;

        Object.Destroy(gameObject);
    }

    public void RunTimer()
    {
        if (thrown)
        {
            Throw();
        }
        else
        {
            projectile.GetComponent<MeshRenderer>().enabled = false;
        }
        if (runStun)
        {
            timer += Time.deltaTime;
            Debug.Log("Running Timer");
            if (timer >= stunTime)
            {
                RemoveStun();
            }
        }
    }

    public void Aim()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (aiming)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(startPos, hit.point) <= aimRange)
                {
                    targetPos.position = hit.point;
                    targetPos.gameObject.GetComponent<MeshRenderer>().enabled = true;
                }
                else
                {
                    targetPos.gameObject.GetComponent<MeshRenderer>().enabled = false;
                }

            }
        }
        else
        {
            targetPos.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void Throw()
    {
        projectile.GetComponent<MeshRenderer>().enabled = true;
        float x0 = startPos.x;
        float x1 = targetPos.position.x;
        float xDist = x1 - x0;
        float z0 = startPos.z;
        float z1 = targetPos.position.z;
        float zDist = z1 - z0;

        float nextX = Mathf.MoveTowards(projectile.transform.position.x, x1, speed * Time.deltaTime);
        float nextZ = Mathf.MoveTowards(projectile.transform.position.z, z1, speed * Time.deltaTime);

        float baseYX = Mathf.Lerp(startPos.y, targetPos.position.y, (nextX - x0) / xDist);
        float baseYZ = Mathf.Lerp(startPos.y, targetPos.position.y, (nextZ - z0) / zDist);
        float baseY = (baseYX + baseYZ) / 2;

        float arcX = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * xDist * xDist);
        float arcZ = arcHeight * (nextZ - z0) * (nextZ - z1) / (-0.25f * zDist * zDist);
        float arc= (arcX + arcZ) / 2;

        var nextPos = new Vector3(nextX, baseY + arc, nextZ);
        projectile.transform.position = nextPos;

        if (nextPos == targetPos.position)
        {
            thrown = false;
            Arrived();
        }
    }

    private void Arrived()
    {
        Collider[] colliders = Physics.OverlapSphere(targetPos.position, stunRadius);
        collisions = colliders;
        for (int i = 0; i <  collisions.Length; i++)
        {
            if (collisions[i].gameObject.tag.Equals("Player"))
            {
                bool usePC = false;

                if (collisions[i].GetComponent<PlayerCharacterController>() != null)
                {
                    usePC = true;
                }

                if (usePC)
                {
                    var pc = collisions[i].GetComponent<PlayerCharacterController>();
                    pc.speed = 0;
                }
                else
                {
                    var pc = collisions[i].GetComponent<NavMeshAgent>();
                    pc.speed = 0;
                }
            }
        }
        runStun = true;
    }

    private void OnDrawGizmos()
    {
        if (camera != null)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Gizmos.DrawLine(startPos, targetPos.position);
        }
    }
}
