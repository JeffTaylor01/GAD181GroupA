using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    
    public CharacterController controller;
    public float speed;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude>=0.1f)// magnitude of direction vector
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg + cam.eulerAngles.y;
            //Rad2Deg converts radian angle to degrees
            // atan2 gives angle between x axis and 0 terminating at (x,y)
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);
            // smooths down the turning
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f)*Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime); // normalizing the move direction vector when we move 
        }
    }
}
