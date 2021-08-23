using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIcon : MonoBehaviour
{

    public Vector3 rotation;
    public float speed;

    private void Start()
    {
        Vector3.Normalize(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * speed);
    }
}
