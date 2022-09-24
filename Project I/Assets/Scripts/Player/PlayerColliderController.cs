using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    Blinky_Pinky_ClydeMovementController _ghostMovementController;
    Blinky_Pinky_Inky_StateController _ghostStateController;
    PlayerStateController _playerStateController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ghosts"))
        {
            _ghostMovementController = other.gameObject.GetComponent<Blinky_Pinky_ClydeMovementController>();
            _ghostStateController = other.gameObject.GetComponentInChildren<Blinky_Pinky_Inky_StateController>();
            _playerStateController = this.transform.parent.gameObject.GetComponentInChildren<PlayerStateController>();

            if (_playerStateController.CheckCurrentState(PlayerState.powerUp) && _ghostStateController.CheckCurrentState(GhostState.frightened))
            {
                PlayerKillGhost();
            }
            else if(_ghostStateController.CheckCurrentState(GhostState.scatter) || _ghostStateController.CheckCurrentState(GhostState.chase))
            {
                GhostKillPlayer();
            }
        }
    }

    private void GhostKillPlayer()
    {
        _playerStateController.Dead();
        _ghostMovementController.GhostStopOrNot(true);
    }

    private void PlayerKillGhost()
    {
        _ghostStateController.TurnToEatenState();
    }
}
