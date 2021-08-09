using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderToUI : MonoBehaviour
{
    public string setting;
    public bool isFloat;
    public TextMeshProUGUI ui;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        if (PlayerPrefs.HasKey(setting))
        {
            if (isFloat)
            {
                slider.value = PlayerPrefs.GetFloat(setting);
            }
            else
            {
                slider.value = PlayerPrefs.GetInt(setting);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFloat)
        {
            ui.text = "" + slider.value.ToString("F1");
        }
        else
        {
            ui.text = "" + slider.value;
        }        
    }
}
