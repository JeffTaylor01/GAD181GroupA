using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContestantManager : MonoBehaviour
{

    public float ceaseFire = 60;
    public float timer = 0;

    public int AIAmount = 5;
    public GameObject prefab;
    public GameObject[] pseudos;
    public Material taggerColor;

    public GameObject tagger;
    private bool selectIT = true;


    // Start is called before the first frame update
    void Start()
    {
        pseudos = new GameObject[AIAmount];

        for (int i = 0; i < AIAmount; i++)
        {
            pseudos[i] = Instantiate(prefab, new Vector3(0, 1.35f, i * 1.5f), Quaternion.identity) as GameObject;
            pseudos[i].name = "Pseudo" + (i+1);
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (timer < ceaseFire && selectIT == true)
        {
            timer += Time.deltaTime;
            if (timer >= ceaseFire)
            {
                int selected = Random.Range(0, AIAmount - 1);
                Debug.Log("IT is Pseudo" + (selected + 1));
                tagger = pseudos[selected];
                //pseudos[selected].GetComponent<MeshRenderer>().material = taggerColor;
                tagger.GetComponent<StateManager>().isIT = true;
                selectIT = false;
            }
        }
    }


}
