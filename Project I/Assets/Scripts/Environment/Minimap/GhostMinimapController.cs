using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMinimapController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _frightenedSprite;
    [SerializeField] private Sprite _frightenNearlyOverSprite;
    [SerializeField] private Sprite _eatenSprite;
    [SerializeField] private Blinky_Pinky_Inky_StateController _ghostStateController;

    [SerializeField] private float _timeToStartFlickering;
    [SerializeField] private float _timeBetweenFlickering;
    [SerializeField] private float _flickerTimer;
    [SerializeField] private bool _switchedColor;

    private void Start()
    {
        _flickerTimer = 0;
        ResetFlickerTimer();
        _switchedColor = false;
    }

    private void Update()
    {
        UpdateMinimap();
    }
    private void UpdateMinimap()
    {
        if (_ghostStateController.CheckCurrentState(GhostState.chase) || _ghostStateController.CheckCurrentState(GhostState.scatter))
        {
            _spriteRenderer.sprite = _normalSprite;
            _switchedColor = false;
            ResetFlickerTimer();

        }
        else if (_ghostStateController.CheckCurrentState(GhostState.frightened))
        {
            if (_ghostStateController.FrightenNearlyOver(_timeToStartFlickering))
            {
                CheckIfSwitchOrNot();
                if (!_switchedColor)
                {
                    _spriteRenderer.sprite = _frightenedSprite;
                }
                else
                {
                    _spriteRenderer.sprite = _frightenNearlyOverSprite;
                }
            }
            else
            {
                _spriteRenderer.sprite = _frightenedSprite;
            }
        }
        else if (_ghostStateController.CheckCurrentState(GhostState.eaten))
        {
            _spriteRenderer.sprite = _eatenSprite;
            _switchedColor = false;
            ResetFlickerTimer();
        }
    }

    private void CheckIfSwitchOrNot()
    {
        if (IsFlickerTimeOut(_timeBetweenFlickering))
        {
            ResetFlickerTimer();
            _switchedColor = !_switchedColor;
        }
        else
        {
            FlickerTimeRun();
        }
    }

    private bool IsFlickerTimeOut(float timeToFlick)
    {
        return _flickerTimer > timeToFlick;
    }

    private void ResetFlickerTimer()
    {
        _flickerTimer = 0;
    }

    private void FlickerTimeRun()
    {
        _flickerTimer +=Time.deltaTime;
    }
}
