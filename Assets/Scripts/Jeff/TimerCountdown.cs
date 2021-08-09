using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerCountdown : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public int secondsLeft = 20;
    public bool takingAway = false;
    public ContestantManager info;

    void Start()
    {
        info = GameObject.FindGameObjectWithTag("GameController").GetComponent<ContestantManager>();
        textDisplay.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        int timeLeft = (int) (info.eliminationTime - info.elimTimer);
        textDisplay.text = ""+timeLeft;
    }

    

}
