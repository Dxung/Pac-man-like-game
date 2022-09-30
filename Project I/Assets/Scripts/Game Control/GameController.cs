using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PacmanHealthCounter _pacmanHealthCounter;
    [SerializeField] private PelletCounter _pelletCounter;
    [SerializeField] private FirstPersonController _pacmanController;
    [SerializeField] private PlayerColliderController _pacmanCollider;
    [SerializeField] private GameObject[] _ghosts;

    [SerializeField] private GameObject _loseBoard;
    [SerializeField] private GameObject _winBoard;


    public void CheckIfGameOver()
    {
        if (_pacmanHealthCounter.CheckPacmanDeath())
        {
            StopAllGhost();
            TurnToLoseScene();
            _pacmanCollider.IsGhostTouched(false);
        }
        else
        {
            _pacmanHealthCounter.UpdatePacmanHealthUI();
            StartCoroutine(RespawnPlayerAndGhost());       
        }
    }

    public void CheckIfgameWin()
    {
        if (_pelletCounter.IsPelletAllOut())
        {
            StopAllGhost();
            TurnToWinScene();
        }
    }

    IEnumerator RespawnPlayerAndGhost()
    {
        StopAllGhost();
        yield return new WaitForSeconds(2);
        _pacmanController.ReSpawnPacman();
        foreach (GameObject ghost in _ghosts)
        {
            ghost.GetComponent<Blinky_Pinky_ClydeMovementController>().RespawnGhostMovement();
            ghost.GetComponentInChildren<Blinky_Pinky_Inky_StateController>().ReSpawnGhostState();
        }
        _pacmanCollider.IsGhostTouched(false);

    }

    public void TurnToWinScene()
    {
        _winBoard.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        PauseMenu._gameIsPaused = true;

    }

    public void TurnToLoseScene()
    {

        _loseBoard.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        PauseMenu._gameIsPaused = true;

    }

    private void StopAllGhost()
    {
        foreach (GameObject ghost in _ghosts)
        {
            ghost.GetComponent<Blinky_Pinky_ClydeMovementController>().GhostStop();
        }
    }
}
