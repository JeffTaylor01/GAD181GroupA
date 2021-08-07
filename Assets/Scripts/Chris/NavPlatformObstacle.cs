using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPlatformObstacle : MonoBehaviour
{
    public bool inverse;
    public float maxLength;
    public float maxHeight;
    public MovingPlatform platform;

    private float startHeight;
    private GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        startHeight = transform.position.y;
        SetScale();
    }

    // Update is called once per frame
    void Update()
    {
        SetScale();
    }

    private void SetScale()
    {
        float percentage = 0;
        if (inverse)
        {
            percentage = 1 - (platform.distance / platform.maxDistance);
        }
        else
        {
            percentage = platform.distance / platform.maxDistance;
        }
        transform.localScale = new Vector3(1, 1, maxLength * percentage);
        if (percentage <= 0.01)
        {
            var pos = transform.position;
            pos.y = startHeight + maxHeight * percentage;
            transform.position = pos;
        }
        else
        {
            var pos = transform.position;
            pos.y = maxHeight;
            transform.position = pos;
        }
        
    }
}
