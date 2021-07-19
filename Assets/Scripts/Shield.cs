using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public bool destroyOnEnd;
    public GameObject prefab;
    public bool runShield;
    public float shieldTime = 6;
    public float timer;

    private GameObject contestant;
    private GameObject shield;
    private StateManager stateInfo;

    private Collider col;
    private MeshRenderer mesh;

    // Start is called before the first frame update
    private void Start()
    {
        col = GetComponent<Collider>();
        mesh = GetComponent<MeshRenderer>();
        timer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (runShield)
        {
            timer += Time.deltaTime;
            Debug.Log("Running Timer");
            if (timer >= shieldTime)
            {
                RemoveShield(contestant);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log(other.gameObject.name + " touched ShieldItem");
            contestant = other.gameObject;
            ShieldContestant(contestant);

            if (destroyOnEnd)
            {
                col.enabled = false;
                mesh.enabled = false;
            }
        }
    }

    private void ShieldContestant(GameObject character)
    {
        stateInfo = character.GetComponent<StateManager>();
        stateInfo.shielded = true;

        shield = Instantiate(prefab, character.transform);

        runShield = true;
        timer = 0;
    }

    private void RemoveShield(GameObject character)
    {
        Object.Destroy(shield);
        stateInfo.shielded = false;
        runShield = false;

        if (destroyOnEnd)
        {
            Object.Destroy(gameObject);
        }
    }
}
