using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class ContestantManager : MonoBehaviour
{

    public bool spawnAI;
    public bool includePlayer;
    public GameObject player;
    public GameSettings settings;

    public bool hasCeaseFire;
    public float ceaseFire = 60;
    public float cfTimer = 0;

    public bool hasElimination;
    public bool runElimination;
    public float eliminationTime = 20;
    public float elimTimer = 0;

    public int AIAmount = 5;
    public bool spawnRandom;
    public float randomRadius;
    public Vector3 spawnSpacing;
    public GameObject prefab;
    public List<GameObject> contestants;

    public GameObject tagger;
    private bool selectIT = true;
    private bool hasWinner;
    private GameObject winner;
    public GameObject winText;
    public GameObject loseScreen;

    public AudioSource bgm;
    public AudioSource win;
    public AudioSource lose;
    public AudioSource tag;
    public AudioSource ow;

    // Start is called before the first frame update
    void Start()
    {
        if (loseScreen != null)
        {
            loseScreen.SetActive(false);
        }
        if (winText != null)
        {
            winText.SetActive(false);
        }        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        var count = AIAmount;

        if (settings != null)
        {
            count = PlayerPrefs.GetInt("AIAmount");
            AIAmount = PlayerPrefs.GetInt("AIAmount");
        }

        if (spawnAI)
        {
            if (includePlayer)
            {
                contestants.Add(player);                
            }
            for (int i = 0; i < count; i++)
            {
                Vector3 finalPosition;
                if (spawnRandom)
                {
                    Vector3 randomDirection = Random.insideUnitSphere * randomRadius;
                    randomDirection += transform.position;
                    NavMeshHit hit;
                    NavMesh.SamplePosition(randomDirection, out hit, randomRadius, 1);
                    finalPosition = hit.position;
                    finalPosition.y += 1;
                }
                else
                {
                    finalPosition = gameObject.transform.position;
                    finalPosition += spawnSpacing * i;
                }

                var contestant = Instantiate(prefab, finalPosition, Quaternion.identity) as GameObject;
                contestant.name = "Pseudo" + (i + 1);
                contestants.Add(contestant);
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            CeaseFire();
            Winner();
            Elimination();            
        }
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
                    if (hasElimination)
                    {
                        runElimination = true;
                    }
                    else
                    {
                        runElimination = false;
                    }
                }
            }
        }
    }

    private void Elimination()
    {
        if (runElimination)
        {
            if (contestants.Count > 1)
            {
                elimTimer += Time.deltaTime;
                if (elimTimer >= eliminationTime)
                {                    
                    selectIT = true;
                    if (contestants.Count == 1 && contestants.Contains(player))
                    {
                        hasWinner = true;
                        bgm.Stop();
                        winText.SetActive(true);
                        win.Play();
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        Time.timeScale = 0;
                    }
                    else if (!contestants.Contains(player) && includePlayer)
                    {
                        hasWinner = true;
                        loseScreen.SetActive(true);
                        var pause = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
                        pause.canPause = false;
                        bgm.Stop();
                        lose.Play();
                        elimTimer = 20;                        
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        Time.timeScale = 0;
                    }
                    contestants.Remove(tagger);
                    Destroy(tagger);
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
            runElimination = false;
        }
    }
    public void ResetTimer()
    {
        elimTimer = 0;
    }
}
