using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
   
    [SerializeField] private GameObject pausePanel;
    public PlayerCameraControlelr camera;
    public bool canPause;

    void Start()
    {
        ContinueGame();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canPause)
            {
                if (!pausePanel.activeInHierarchy)
                {
                    PauseGame();
                }
                else if (pausePanel.activeInHierarchy)
                {
                    ContinueGame();
                }
            }
        }
    }
    
    public void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        camera.enabled = false;
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame()
    {
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        camera.enabled = true;
        //enable the scripts again
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}