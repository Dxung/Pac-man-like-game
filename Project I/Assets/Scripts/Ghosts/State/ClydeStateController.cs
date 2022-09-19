using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeStateController : Blinky_Pinky_Inky_StateController
{
    [SerializeField] private float _switchingArea;
    [SerializeField] private Transform _playerPosition;


    

    protected override void UpdateGhostState()
    {
        if (CheckCurrentState(GhostState.chase) || CheckCurrentState(GhostState.scatter))
        {
            if (OutOfSwitchingArea())
            {
                ChangeGhostState(GhostState.chase);
            }
            else
            {
                ChangeGhostState(GhostState.scatter);
            }
        }

        if (CheckCurrentState(GhostState.frightened))
        {
            UpdateFrightenedMode();
        }
    }

    //check if clyde get close enough to player to switch state ?
    // if true: ghost is far from player --> chase
    //if false: ghost is close to player --> scatter
    private bool OutOfSwitchingArea ()
    {
        return GetDistance() > _switchingArea; 
    }

    //get the distance between ghost and player
    private float GetDistance()
    {
        return Vector3.Distance(this.transform.position, _playerPosition.position);
    }
    
}
