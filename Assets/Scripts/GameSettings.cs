using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{

    public Slider aiCount;
    public Slider sensitivity;

    public int AI;
    public float sen;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("AIAmount"))
        {
            PlayerPrefs.SetInt("AIAmount", 5);
        }
        if (!PlayerPrefs.HasKey("Sensitivity"))
        {
            PlayerPrefs.SetFloat("Sensitivity", 1f);
        }        
    }

    private void Update()
    {
        AI = PlayerPrefs.GetInt("AIAmount");
        sen = PlayerPrefs.GetFloat("Sensitivity");
    }

    // Update is called once per frame
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("AIAmount", (int)aiCount.value);
        PlayerPrefs.SetFloat("Sensitivity", sensitivity.value);
        PlayerPrefs.Save();
    }
}
