using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject[] items;
    public GameObject item;

    bool itemSpawned;
    bool runCooldown;
    public float cooldownTime;
    public float timer;

    public Material mat;


    // Start is called before the first frame update
    void Start()
    {
        item = null;
        mat = GetComponent<MeshRenderer>().material;
        spawnItem();
    }

    // Update is called once per frame
    void Update()
    {

        if (itemSpawned)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.yellow;
        }

        if (runCooldown)
        {
            timer += Time.deltaTime;
            if (timer >= cooldownTime)
            {
                runCooldown = false;
                spawnItem();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (itemSpawned)
        {
            if (other.GetComponent<StateManager>() != null)
            {
                if (other.gameObject.GetComponent<StateManager>().heldItem == null)
                {
                    runCooldown = true;

                    addItemToPlayer(other.gameObject);
                }
            }                           
        }        
    }

    private void spawnItem()
    {
        Debug.Log("New Item Spawned");
        item = items[Random.Range(0, items.Length)];
        itemSpawned = true;
    }

    private void addItemToPlayer(GameObject player)
    {
        if (player.GetComponent<StateManager>() != null)
        {
            GameObject it = Instantiate(item, player.transform.position, player.transform.rotation) as GameObject;
            it.transform.parent = player.transform;
            it.name = item.name;

            player.GetComponent<StateManager>().heldItem = it;
            itemSpawned = false;
            item = null;
        }        
    }
}
