using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charJump : MonoBehaviour
{
    public float jumpHeight = 6f;
    public bool isGrounded;
    public float jumpForce = 5.0f;

    private  Rigidbody playerRb; 

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            }
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }
    }