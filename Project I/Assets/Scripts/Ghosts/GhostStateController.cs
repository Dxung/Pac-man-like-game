using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GhostState
{
    chase,
    scatter,
    frightened,
    eaten
}

//this script contains methods that control the state system of all ghost
public class GhostStateController : MonoBehaviour
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


    private void Update()
    {
        UpdateGhostState();
    }


    /// ~~TRIGGER CHANGING STATE~~

    //Used when change ghost state to FrightenedMode
    //Pause Timer
    //reset timer to 0
    //Change State to Frightened
    public void TurnToFrightenedState()
    {
        PauseGhostStatus();
        ResetTimer();
        ChangeGhostState(GhostState.frightened);
    }

    //Used when change ghost state to EatenMode
    //Change State to Eaten
    public void TurnToEatenState()
    {
        ChangeGhostState(GhostState.eaten);
    }

    public void FinishEatenState()
    {
        ResumeGhostStatus();
    }


    /// ~~UPDATING STATE EVERY FRAME~~

    private void UpdateGhostState()
    {
        if (CheckCurrentState(GhostState.scatter))
        {
            UpdateScatterMode();
        }

        //after iteration go to 4, ghost will be in chase for the rest of the game (not counting frightened/eaten)
        if (CheckCurrentState(GhostState.chase))
        {
            if (_modeSwitchIteration == 1 || _modeSwitchIteration == 2 || _modeSwitchIteration == 3)
            {
                UpdateChaseMode();
            }
        }
        if (CheckCurrentState(GhostState.frightened))
        {
            UpdateFrightenedMode();
        }
    }

    // check if time for scatter mode at that iteration is over?
    //no? : continue counting down
    //yes? : switch other state & reset timer
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

    // check if time for chase mode at that iteration is over?
    //no? : continue counting down
    //yes? : switch other state & reset timer & to the next "switch iteration"
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

    //check if time for frighten mode is over?
    //no? : continue counting down
    //yes? : switch back to previous state and timer
    private void UpdateFrightenedMode()
    {
        if (IsTimeOut(_stateModeTime.GetFrightenedModeTime()))
        {
            ResumeGhostStatus();
            
        }
        else
        {
            TimeRun();
        }
    }




    /// ~~Pause/Resume previous state~~

    //Used when ghost state change to frightened mode/eaten mode
    //save current Timer
    //save current ghost state
    private void PauseGhostStatus()
    {
        _previousTimer = _currentTimer;
        _previousGhostState = _currentGhostState;
    }

    //Used when ghost state finish its frightened mode/ eaten mode
    //set Timer back to where it stops 
    //set state back to where it stops
    private void ResumeGhostStatus()
    {
        _currentTimer = _previousTimer;
        ChangeGhostState(_previousGhostState);
    }



 
                                                        /// ~~GETTER AND SETTER~~

    private void ChangeGhostState(GhostState ghostStateToChangeTo)
    {
        _currentGhostState = ghostStateToChangeTo;
    }

    private bool CheckCurrentState(GhostState ghostStateToCheck)
    {
        return _currentGhostState == ghostStateToCheck;
    }

    private void ToNextIteration()
    {
        _modeSwitchIteration++;
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
        _currentTimer++;
    }

}
