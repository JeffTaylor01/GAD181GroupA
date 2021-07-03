using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    [SerializeField] Transform groundCheck = null;
    [SerializeField] LayerMask playerMask;
    [SerializeField] float movementSpeed = 5;
    [SerializeField] int jumpHeight = 5;
    [SerializeField] float boostingTime = 3;

    bool jumpPressed;
    float horizontalInput;
    float verticalInput;
    float speedBoost;
    float boostTimer = 0;
    bool isBoosting = false;
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
        
    }

    internal static void move(Vector3 vector3)
    {
        throw new NotImplementedException();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontalInput * (movementSpeed+speedBoost), rb.velocity.y, verticalInput * (movementSpeed+speedBoost));
        if (isBoosting)
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= boostingTime)
            {
                speedBoost = 0;
                boostTimer = 0;
                isBoosting = false;
            }
        }
        if (Physics.OverlapSphere(groundCheck.position, 0.1f, playerMask).Length != 0)
        {
            if (jumpPressed)
            {
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
                jumpPressed = false;
            }            
        }
        
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SpeedBooster")
        {
            isBoosting = true;
            speedBoost = 10;
        }
    }

}