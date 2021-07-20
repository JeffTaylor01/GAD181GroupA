using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControlelr : MonoBehaviour
{

    public float rotSpeed = 1;
    public Transform target, player;

    private float mouseX;
    private float mouseY;

    private bool seeMouse = false;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void LateUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!seeMouse)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                seeMouse = true;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                seeMouse = false;
            }
            
        }

        CamControl();
    }

    private void CamControl()
    {

        mouseX += Input.GetAxis("Mouse X") * rotSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotSpeed;

        mouseY = Mathf.Clamp(mouseY, -25, 60);
        transform.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
