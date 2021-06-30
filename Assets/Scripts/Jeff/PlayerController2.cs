using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    float h;
    float v;
    public float speed;
    public float movement;
    public float jump;
    private float velocityY;

    void Start()
    {

    }

    void Update()
    {
        velocityY -= 9.81f * Time.deltaTime;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        var movementVector = transform.forward * v * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocityY = jump;
        }
        movementVector.y = velocityY;
        GetComponent<CharacterController>().Move(movementVector);
        transform.Rotate(0, h, 0);


    }
}
