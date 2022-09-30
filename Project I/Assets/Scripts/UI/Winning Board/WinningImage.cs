using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinningImage : MonoBehaviour
{
    [SerializeField] private Sprite[] _imageListToSwitch;
    [SerializeField] private Image _imageSwitchTo;
    [SerializeField] private float _secondBetweenWait;

    private int _currentImageNumber;
    private float _currentTime;
    private void Start()
    {
        _currentImageNumber = 0;
    }


    private void Update()
    {
        UpdateImage();
    }

    private void  UpdateImage()
    {
        if (IsTimeToSwitch())
        {
            _imageSwitchTo.sprite = _imageListToSwitch[_currentImageNumber];
            WhatIsNextImageNumber();
            ResetTimer();

        }
        else
        {
            _currentTime += Time.unscaledDeltaTime*1f;
        }


        
    }

    private void WhatIsNextImageNumber()
    {
        if(_currentImageNumber == _imageListToSwitch.Length - 1)
        {
            _currentImageNumber = 0;
        }
        else
        {
            _currentImageNumber++;
        }
    }

    private bool IsTimeToSwitch()
    {
        return _currentTime > _secondBetweenWait;
    }

    private void ResetTimer()
    {
        _currentTime = 0;
    }

}
