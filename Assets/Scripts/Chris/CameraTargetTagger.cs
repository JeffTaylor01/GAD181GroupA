using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetTagger : MonoBehaviour
{

    private ContestantManager info;
    public GameObject camera;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        info = GameObject.FindGameObjectWithTag("GameController").GetComponent<ContestantManager>();
    }

    // Update is called once per frame
    void Update()
    {
        target = info.tagger.transform;
        transform.position = target.position;

        camera.transform.LookAt(this.transform);
        camera.transform.Translate(Vector3.right * Time.deltaTime);
    }
}
