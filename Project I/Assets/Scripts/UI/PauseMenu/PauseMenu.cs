using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool _gameIsPaused = false;

    public GameObject _pauseMenuUI;
    public GameObject _pauseOptions;
    public GameObject _settingOptions;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        _pauseOptions.SetActive(true);
        _settingOptions.SetActive(false);
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
