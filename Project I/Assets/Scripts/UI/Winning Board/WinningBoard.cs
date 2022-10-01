using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class WinningBoard : MonoBehaviour
{
    [SerializeField] public ScoreCounter _scoreCounter;
    [SerializeField] public TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        _textMeshPro.text = _scoreCounter.GetHighScore().ToString();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("StartingMenu");
    }

    public void ReloadScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        PauseMenu._gameIsPaused = false;
        SceneManager.LoadScene("Debug Scene");
    }
}
