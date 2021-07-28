using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseGame : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject PausemenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            void Resume()
            {
                PausemenuUI.SetActive(false);
                Time.timeScale = 1f;
                GameIsPaused = false;
            }
            void Pause()
            {
                PausemenuUI.SetActive(true);
                Time.timeScale = 0f; // freezes time in pause
                GameIsPaused = true;
            }
        }
    }
}
