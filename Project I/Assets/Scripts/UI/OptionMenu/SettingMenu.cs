using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer _audioMixer;
    public Image _toggleStatusImage;
    public Image _ghostTraceToggle;

    public Resolution[] _resolutions;
    public TMP_Dropdown _resolutionDropDown;

    public GameObject _debugLine;

    private void Start()
    {
        ///Use these to put all the available screen resolution's name from your device to the dropdown list (clear all existing option there firstly) 

        _resolutions = Screen.resolutions;

        _resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i=0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height;
            options.Add(option);

            if(_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionDropDown.AddOptions(options);
        _resolutionDropDown.value = currentResolutionIndex;
        _resolutionDropDown.RefreshShownValue();
    }

    public void SetGameVolume(float volume)
    {
        _audioMixer.SetFloat("volume", volume);
    }

    public void EnterDebug()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        PauseMenu._gameIsPaused = false;
        SceneManager.LoadScene("Debug Scene");
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SwitchToggleStatusImage(bool _isFullScreen)
    {    
            _toggleStatusImage.enabled = _isFullScreen;      
    }

    public void SwitchGhostTraceImage(bool _isGhostTraceOn)
    {
        _ghostTraceToggle.enabled = _isGhostTraceOn;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void TurnDebugLineOnOrOff(bool whichToTurn)
    {
        _debugLine.SetActive(whichToTurn);
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
