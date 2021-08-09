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
    public float cfTimer = 0;

    public bool hasElimination;
    public float eliminationTime = 20;
    public float elimTimer = 0;

    public int AIAmount = 5;
    public Vector3 spawnSpacing;
    public GameObject prefab;
    public List<GameObject> contestants;

    public GameObject tagger;
    private bool selectIT = true;
    private bool hasWinner;
    private GameObject winner;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnAI)
        {
            if (includePlayer)
            {
                contestants.Add(player);                
            }
            for (int i = 0; i < AIAmount; i++)
            {
                var contestant = Instantiate(prefab, gameObject.transform.position + new Vector3(spawnSpacing.x * i - 1, spawnSpacing.y, spawnSpacing.z * i - 1), Quaternion.identity) as GameObject;
                contestant.name = "Pseudo" + (i + 1);
                contestants.Add(contestant);
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        CeaseFire();
        Winner();
        Elimination();
    }

    private void CeaseFire()
    {
        if (hasCeaseFire)
        {
            if (cfTimer < ceaseFire)
            {
                cfTimer += Time.deltaTime;
                if (cfTimer >= ceaseFire)
                {
                    SelectTagger();
                    hasElimination = true;
                }
            }
        }
    }

    private void Elimination()
    {
        if (hasElimination)
        {
            if (contestants.Count > 1)
            {
                elimTimer += Time.deltaTime;
                if (elimTimer >= eliminationTime)
                {
                    contestants.Remove(tagger);
                    Destroy(tagger);
                    selectIT = true;
                    SelectTagger();
                    elimTimer = 0;
                }
            }            
        }
    }

    private void SelectTagger()
    {
        if (selectIT && !hasWinner)
        {
            int selected = Random.Range(0, contestants.Count);

            tagger = contestants[selected];
            Debug.Log("IT is" + tagger.name);

            tagger.GetComponent<StateManager>().isIT = true;
            selectIT = false;
        }        
    }

    private void Winner()
    {
        if (contestants.Count > 1)
        {
            hasWinner = false;
        }
        else if (contestants.Count == 1)
        {
            hasWinner = true;
            winner = contestants[0];
            winner.GetComponent<StateManager>().isIT = false;
            tagger = null;
            hasElimination = false;
        }
    }
}
