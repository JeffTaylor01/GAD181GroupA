using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public Vector3 moveDirection;
    public float maxDashTime;
    public float dashSpeed;
    public float dashStoppingSpeed;

    private float currentDashTime;

    void Start()
    {
        currentDashTime = maxDashTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentDashTime = 0.0f;
        }
        if (currentDashTime < maxDashTime)
        {
            moveDirection = new Vector3(0, 0, dashSpeed);
            currentDashTime += dashStoppingSpeed;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        PlayerController.move(moveDirection * Time.deltaTime);
    }

    
}
