using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocityMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float h, v;
    public float speed;
    public float jumpSpeed;
    public float maxSpeed;
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        velocity += transform.forward * v * speed;

        velocity.y = 0;
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
        velocity.y = GetComponent<Rigidbody>().velocity.y;
        GetComponent<Rigidbody>().velocity = velocity;


        this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, h, 0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpSpeed;
        }
    }
}


