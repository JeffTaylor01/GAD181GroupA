using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlipColour : MonoBehaviour
{

    public float yLevel;
    private MeshRenderer blip;
    public MeshRenderer player;

    private void Start()
    {
        blip = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (blip.material.color != player.material.color)
        {
            blip.material.color = player.material.color;
        }
        var pos = transform.position;
        pos.y = yLevel;
        transform.position = pos;
    }
}
