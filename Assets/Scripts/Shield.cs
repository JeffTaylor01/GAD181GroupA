using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public GameObject prefab;
    public bool runShield;
    public float shieldTime = 6;
    public float timer;

    public GameObject user;
    private GameObject shield;
    private StateManager stateInfo;

    public void UseItem()
    {
        stateInfo = user.GetComponent<StateManager>();
        stateInfo.shielded = true;
        stateInfo.itemUsed = true;

        shield = Instantiate(prefab, user.transform);

        runShield = true;
        timer = 0;
    }

    private void RemoveShield()
    {
        Object.Destroy(shield);
        stateInfo.shielded = false;
        stateInfo.heldItem = null;
        runShield = false;

        Object.Destroy(gameObject);
    }

    public void RunTimer()
    {
        if (runShield)
        {
            timer += Time.deltaTime;
            Debug.Log("Running Timer");
            if (timer >= shieldTime)
            {
                RemoveShield();
            }
        }
    }
}
