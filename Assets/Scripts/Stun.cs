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
    private bool userPC;

    public Transform targetPos;
    public Vector3 startPos;
    public float stunRadius = 8;
    public float aimRange = 25;

    public AnimationCurve arc;
    private float throwTime = 0;
    private float distance;

    private Collider[] collisions;

    private void Awake()
    {
        targetPos.localScale = new Vector3(stunRadius * 2, stunRadius * 2, stunRadius * 2);
    }

    private void CheckUser()
    {
        if (user.GetComponent<PlayerCharacterController>() != null)
        {
            userPC = true;
        }
        else
        {
            userPC = false;
        }
    }

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
                collisions[i].gameObject.GetComponent<StateManager>().stunned = false;
                bool usePC = false;

                if (collisions[i].GetComponent<PlayerCharacterController>() != null)
                {
                    usePC = true;
                }

                if (usePC)
                {
                    var pc = collisions[i].GetComponent<PlayerCharacterController>();
                    pc.speed = 35;
                }
                else
                {
                    var pc = collisions[i].GetComponent<NavMeshAgent>();
                    pc.speed = 40;
                }
            }
        }

        runStun = false;
        stateInfo.itemUsed = false;
        stateInfo.heldItem = null;

        Object.Destroy(targetPos.gameObject);
        Object.Destroy(projectile);
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
        CheckUser();
        if (userPC)
        {
            var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (aiming)
            {
                Debug.Log("Player Aiming");
                if (Physics.Raycast(ray, out hit))
                {
                    if (Vector3.Distance(startPos, hit.point) <= aimRange)
                    {
                        var vel = Vector3.zero;
                        var pos = Vector3.SmoothDamp(targetPos.position, hit.point, ref vel, 1);
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
        else
        {
            if (aiming)
            {
                var aimPos = user.transform.position;
                int xType = Random.Range(0, 1);     // 0 - Negative || 1 - Positive
                float xPos = Random.Range(stunRadius / 2, aimRange);
                if (xType == 0) { xPos *= -1; }
                int zType = Random.Range(0, 1);     // 0 - Negative || 1 - Positive
                float zPos = Random.Range(stunRadius / 2, aimRange);
                if (zType == 0) { zPos *= -1; }

                aimPos.x += xPos;
                aimPos.z += zPos;
                RaycastHit hit;
                Ray ray = new Ray(new Vector3(xPos, 200, zPos), Vector3.down);
                Physics.Raycast(ray, out hit);
                aimPos.y = hit.point.y;

                targetPos.position = aimPos;
            }                
        }
    }

    private void Throw()
    {
        targetPos.parent = null;
        projectile.gameObject.transform.parent = null;
        projectile.GetComponent<MeshRenderer>().enabled = true;

        throwTime += Time.deltaTime;
        Vector3 pos = Vector3.Lerp(startPos, targetPos.position, throwTime);
        pos.y = arc.Evaluate(throwTime) * (Vector3.Distance(startPos, targetPos.transform.position) * .4f);
        projectile.transform.position = pos;

        throwTime += Time.deltaTime;

        if (throwTime >= 1)
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
                collisions[i].gameObject.GetComponent<StateManager>().stunned = true;
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
}
