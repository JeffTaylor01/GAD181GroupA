using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    [SerializeField] Transform groundCheck = null;
    [SerializeField] LayerMask playerMask;
    [SerializeField] int movementSpeed = 5;
    [SerializeField] int jumpHeight = 5;

    bool jumpPressed;
    float horizontalInput;
    float verticalInput;
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Debug.Log(this.transform.forward);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
        if (Physics.OverlapSphere(groundCheck.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }
        if (jumpPressed)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
            jumpPressed = false;
        }

    }

}