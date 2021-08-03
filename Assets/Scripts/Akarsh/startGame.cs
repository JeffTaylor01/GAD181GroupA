using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class startGame : MonoBehaviour
{
    public string level;
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(level);
    }
}
