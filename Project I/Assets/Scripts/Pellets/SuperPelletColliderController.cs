using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPelletColliderController : PelletColliderController
{
    
    //this script will work when pellets be touched by player
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ConsumePellet(other.gameObject);
            PlayPlayerConsumeSound(other.gameObject);
            ChangeplayerState();
            ChangeGhostState();

            ChangePelletStatus();

        }
    }

    protected override void ConsumePellet(GameObject player)
    {
        player.GetComponentInChildren<PlayerColliderController>().ConsumePowerPellet();
    }

    //change player state to consume (if not in powerup)
    protected override void ChangeplayerState()
    {
        PlayerStateController playerStateControl = _StateControllerForPellet.GetPlayerStateController();
        playerStateControl.TurnToPowerUpState();
    }

    private void ChangeGhostState()
    {
        foreach(Blinky_Pinky_Inky_StateController ghostStateController in _StateControllerForPellet.GetGhostStateController())
        {
            ghostStateController.TurnToFrightenedState();

        }
    }


}
