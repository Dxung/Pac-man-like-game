using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GhostState
{
    chase,
    scatter,
    frightened,
    eaten
}

public class Blinky_Pinky_Inky_StateController : MonoBehaviour
{
    [Header("Ghost State")]
    [SerializeField] private GhostState _currentGhostState;

    [Header("Mode Change Timer")]
    [SerializeField] private float _currentTimer = 0;
    [SerializeField] private int _modeSwitchIteration=1;

    [Header("previous status before state change")]
    [SerializeField] private GhostState _previousGhostState;
    [SerializeField] private float _previousTimer;

    [Header("References")]
    [SerializeField] private StateModeTime _stateModeTime;

    protected void Awake()
    {
        ChangeGhostState(GhostState.scatter);
    }

    protected void Update()
    {

        //Debugger();
        UpdateGhostState();
    }


   
    ///after player colliding checking
    public void ReSpawnGhostState()
    {
        ResetIteration();
        ResetTimer();
        ChangeGhostState(GhostState.scatter);
    }



                                                        /// ~~TRIGGER CHANGING STATE~~

    //Used when change ghost state to FrightenedMode
    //cannot change while in eaten state
    public void TurnToFrightenedState()
    {
        if (!CheckCurrentState(GhostState.eaten))
        {
            PauseGhostStatus();
            ResetTimer();
            ChangeGhostState(GhostState.frightened);
        }
    }

    //Used when change ghost state to EatenMode
    //Change State to Eaten
    public void TurnToEatenState()
    {
        if (CheckCurrentState(GhostState.frightened))
        {
            ChangeGhostState(GhostState.eaten);
        }
    }

    public void FinishEatenState()
    {
        ResumeGhostStatus();
    }


                                                        /// ~~UPDATING STATE EVERY FRAME~~

    protected virtual void UpdateGhostState()
    {
        if (CheckCurrentState(GhostState.scatter))
        {
            UpdateScatterMode();
        }

        //after iteration go to 4, ghost will be in chase for the rest of the game (not counting frightened/eaten)
        else if (CheckCurrentState(GhostState.chase))
        {
            if (_modeSwitchIteration == 1 || _modeSwitchIteration == 2 || _modeSwitchIteration == 3)
            {
                UpdateChaseMode();
            }
        }
        else if (CheckCurrentState(GhostState.frightened))
        {
            UpdateFrightenedMode();
        }
    }

    private void UpdateScatterMode()
    {
        if (IsTimeOut(GetScatterModeTimeAt(_modeSwitchIteration)))
        {
            ChangeGhostState(GhostState.chase);
            ResetTimer();
        }
        else
        {
            TimeRun();
        }
    }

    private void UpdateChaseMode()
    {
        if (IsTimeOut(GetChaseModeTimeAt(_modeSwitchIteration)))
        {
            ChangeGhostState(GhostState.scatter);
            ResetTimer();
            ToNextIteration();
        }
        else
        {
            TimeRun();
        }
    }

    protected void UpdateFrightenedMode()
    {
        if (IsTimeOut(_stateModeTime.GetFrightenedPowerUpTime()))
        {
            ResumeGhostStatus();
            
        }
        else
        {
            TimeRun();
        }
    }




                                                        /// ~~PAUSE/RESUME PREVIOUS STAGE~~

    //Use when ghost state change to frightened mode/eaten mode
    //save current Timer
    //save current ghost state
    //do not save frighten/eaten status & timer
    private void PauseGhostStatus()
    {
        
        if (!CheckCurrentState(GhostState.frightened) && !CheckCurrentState(GhostState.eaten))
        {
            CheckCurrentState(GhostState.frightened);
            _previousTimer = _currentTimer;
            _previousGhostState = _currentGhostState;
        }
    }

    //Use when ghost state finish its frightened mode/ eaten mode
    //set Timer back to where it stops 
    //set state back to where it stops
    private void ResumeGhostStatus()
    {
        _currentTimer = _previousTimer;
        ChangeGhostState(_previousGhostState);
    }



 
                                                        /// ~~GETTER AND SETTER~~

    protected void ChangeGhostState(GhostState ghostStateToChangeTo)
    {
        _currentGhostState = ghostStateToChangeTo;
    }

    public bool CheckCurrentState(GhostState ghostStateToCheck)
    {
        return _currentGhostState == ghostStateToCheck;
    }

    private void ToNextIteration()
    {
        _modeSwitchIteration++;
    }

    private void ResetIteration()
    {
        _modeSwitchIteration = 1;
    }

    //get the appropriate mode time at which iteration from the data "statemodetime"
    private int GetScatterModeTimeAt(int iteration)
    {
        return _stateModeTime.GetScatterModeTimeAt(iteration);
    }

    //get the appropriate mode time at which iteration from the data "statemodetime"
    private int GetChaseModeTimeAt(int modeSwitch)
    {
        return _stateModeTime.GetChaseModeTimeAt(modeSwitch);
    }


                                                        /// ~~TIMER'S METHODS~~

    private void ResetTimer()
    {
        _currentTimer = 0;
    }

    //Used to check if the time for that mode is finished
    private bool IsTimeOut(int timeForThatMode)
    {
        return _currentTimer > timeForThatMode;
    }

    //used every frame in a duration of a mode
    private void TimeRun()
    {
        _currentTimer += Time.deltaTime;
    }

    public bool FrightenNearlyOver(float secondsleft)
    {
        float secondsRunned = _stateModeTime.GetFrightenedPowerUpTime() - secondsleft;
        return _currentTimer > secondsRunned;
    }

    /// Debug Only
    //[SerializeField] private bool _check;
    //[SerializeField] private bool _checked;


    //private void Debugger()
    //{
    //    if (_check)
    //    {
    //        if (!_checked)
    //        {
    //            _checked = true;
    //            Check();
    //        }
    //    }
    //}
    //private void Check()
    //{
    //    TurnToFrightenedState();
    //}

}
