using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject[] holograms;
    public GameObject[] items;
    public GameObject item;

    public bool itemSpawned;
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

        if (!itemSpawned && !runCooldown)
        {
            spawnItem();
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
        TriggerSpawner(other);     
    }

    private void OnTriggerStay(Collider other)
    {
        TriggerSpawner(other);
    }

    private void TriggerSpawner(Collider other)
    {
        if (itemSpawned)
        {
            if (other.tag.Equals("Player"))
            {
                //Debug.Log("Player touched spawner");
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
        int selected = Random.Range(0, items.Length);
        item = items[selected];
        holograms[selected].SetActive(true);
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
            foreach (GameObject icon in holograms)
            {
                icon.SetActive(false);
            }
        }        
    }
}
