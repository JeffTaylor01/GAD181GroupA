using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float h, v;
    public float speed = 7;
    public float jump = 6;
    float velocityY = 0;    // Update is called once per frame
    void Update()
    {
        if (GetComponent<CharacterController>().isGrounded == false)
        {
            velocityY -= 9.81f * Time.deltaTime;
        }
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        var movementVector = speed * v * transform.forward;
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
