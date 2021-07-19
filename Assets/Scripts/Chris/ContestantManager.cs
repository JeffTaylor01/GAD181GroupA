using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContestantManager : MonoBehaviour
{

    public bool spawnAI;
    public bool includePlayer;
    public GameObject player;

    public bool hasCeaseFire;
    public float ceaseFire = 60;
    public float timer = 0;

    public int AIAmount = 5;
    public GameObject prefab;
    public GameObject[] contestants;

    public GameObject tagger;
    private bool selectIT = true;


    // Start is called before the first frame update
    void Start()
    {
        if (spawnAI)
        {
            if (includePlayer)
            {
                contestants = new GameObject[AIAmount + 1];
                contestants[0] = player;
                for (int i = 1; i < contestants.Length; i++)
                {
                    contestants[i] = Instantiate(prefab, gameObject.transform.position + new Vector3(0, 1.35f, i * 1.5f), Quaternion.identity) as GameObject;
                    contestants[i].name = "Pseudo" + (i + 1);
                }
            }
            else
            {
                contestants = new GameObject[AIAmount];
                for (int i = 0; i < contestants.Length; i++)
                {
                    contestants[i] = Instantiate(prefab, gameObject.transform.position + new Vector3(0, 1.35f, i * 1.5f), Quaternion.identity) as GameObject;
                    contestants[i].name = "Pseudo" + (i + 1);
                }
            }
            
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCeaseFire)
        {
            if (timer < ceaseFire && selectIT == true)
            {
                timer += Time.deltaTime;
                if (timer >= ceaseFire)
                {
                    int selected = Random.Range(0, contestants.Length - 1);

                    tagger = contestants[selected];
                    Debug.Log("IT is" + tagger.name);

                    tagger.GetComponent<StateManager>().isIT = true;
                    selectIT = false;
                }
            }
        }
        
    }


}
