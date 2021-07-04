using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertestMovement : MonoBehaviour
{
    
    public float h, v;
    public float speed;
    public float jump;
    float velocityY = 0;
    void Update()
    {
        if (GetComponent<CharacterController>().isGrounded == false)
        {
            velocityY -= 9.81f * Time.deltaTime;
        }
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        var movementVector = transform.forward * v * speed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocityY = jump;
        }
        movementVector.y = velocityY;
        movementVector *= Time.deltaTime;
        GetComponent<CharacterController>().Move(movementVector);
        transform.Rotate(0, h, 0);
    }
}
