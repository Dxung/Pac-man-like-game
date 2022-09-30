using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayNewGame()
    {
        
        Time.timeScale = 1f;
        PauseMenu._gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PlayLoadGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
