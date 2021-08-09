using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContestantCount : MonoBehaviour
{

    public ContestantManager contestants;
    public TextMeshProUGUI contestantCounter;

    // Start is called before the first frame update
    void Start()
    {
        contestants = GameObject.FindGameObjectWithTag("GameController").GetComponent<ContestantManager>();
        contestantCounter = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        contestantCounter.text = "" + contestants.contestants.Count;
    }
}
