using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    normal,
    consume,
    powerUp,
    dead
}

public class PlayerStateController : MonoBehaviour
{
    [SerializeField] private StateModeTime _stateModeTime;

    [Header("Player State")]
    [SerializeField] private PlayerState _currentPlayerState;

    [Header("Timer")]
    [SerializeField] private float _currentTimer;

    private void Start()
    {
        ChangeState(PlayerState.normal);
    }

    private void Update()
    {
        UpdatePlayerState();
    }

    private void UpdatePlayerState()
    {
        if (CheckCurrentState(PlayerState.consume))
        {
            UpdateConsumeMode();
        }
        else if (CheckCurrentState(PlayerState.powerUp))
        {
            UpdatePowerUpMode();
        }
    }

    /// UPDATE SETTERS:
    private void UpdateConsumeMode()
    {
        if (IsTimeOut(_stateModeTime.GetConsumeTime()))
        {
            ChangeState(PlayerState.normal);
            ResetTimer();
        }
        else
        {
            TimeRun();
        }
    }

    private void UpdatePowerUpMode()
    {
        if (IsTimeOut(_stateModeTime.GetFrightenedPowerUpTime()))
        {
            ChangeState(PlayerState.normal);
            ResetTimer();
        }
        else
        {
            TimeRun();
        }
    }

    ///GETTERS AND SETTERS:
    public bool CheckCurrentState(PlayerState state)
    {
        return _currentPlayerState == state;
    }

    private void ChangeState(PlayerState state)
    {
        _currentPlayerState = state;
    }

    ///Timer:
    private bool IsTimeOut(int timeForThatState)
    {
        return _currentTimer > timeForThatState;
    }

    private bool IsTimeOut(float timeForThatState)
    {
        return _currentTimer > timeForThatState;
    }

    private void TimeRun()
    {
        _currentTimer += Time.deltaTime;
    }

    private void ResetTimer()
    {
        _currentTimer = 0;
    }

    ///TRIGGERS:
    
    //if not in powerup state, state can be change to consume to slow down speed.
    //if in powerup state, speed can not be changed
    public void TurnToConsumeState()
    {
        if (!CheckCurrentState(PlayerState.powerUp) && !CheckCurrentState(PlayerState.dead))
        {
            ResetTimer();
            ChangeState(PlayerState.consume);
        }
    }

    public void TurnToPowerUpState()
    {
        if (!CheckCurrentState(PlayerState.dead))
        {
            ResetTimer();
            ChangeState(PlayerState.powerUp);
        }
    }

    public void Dead()
    {
        ChangeState(PlayerState.dead);
    }
}
