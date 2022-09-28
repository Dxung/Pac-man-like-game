using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    Blinky_Pinky_ClydeMovementController _ghostMovementController;
    Blinky_Pinky_Inky_StateController _ghostStateController;
    PlayerStateController _playerStateController;

    [SerializeField] PelletCounter _pelletCounter;
    [SerializeField] ScoreCounter _scoreCounter;
    [SerializeField] private GameController _gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ghosts"))
        {
            _ghostMovementController = other.gameObject.GetComponent<Blinky_Pinky_ClydeMovementController>();
            _ghostStateController = other.gameObject.GetComponentInChildren<Blinky_Pinky_Inky_StateController>();
            _playerStateController = this.transform.parent.gameObject.GetComponentInChildren<PlayerStateController>();

            if (_playerStateController.CheckCurrentState(PlayerState.powerUp) && _ghostStateController.CheckCurrentState(GhostState.frightened))
            {
                _ghostStateController.TurnToEatenState();
                AddGhostKillingPoint();
            }
            else if(_ghostStateController.CheckCurrentState(GhostState.scatter) || _ghostStateController.CheckCurrentState(GhostState.chase))
            {
                _playerStateController.Dead();
                GhostKillPacmanUI();
            }
        }
    }

    public void ConsumeSmallPellet()
    {
        _pelletCounter.ConsumeSmallPellet();
        _scoreCounter.AddPointFromSmallPellet();
    }

    public void ConsumePowerPellet()
    {
        _pelletCounter.ConsumePowerPellet();
        _scoreCounter.AddPointFromPowerPellet();
    }

    private void AddGhostKillingPoint()
    {
        _scoreCounter.AddPointFromGhost();
    }
    
    private void GhostKillPacmanUI()
    {
        _gameController.CheckIfGameOver();
    }

}
