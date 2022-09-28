using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PacmanHealthCounter _pacmanHealthCounter;
    [SerializeField] private FirstPersonController _pacmanController;
    [SerializeField] private GameObject[] _ghosts;


    public void CheckIfGameOver()
    {
        if (_pacmanHealthCounter.CheckPacmanDeath())
        {
            StopAllGhost();
            TurnToLoseScene();
        }
        else
        {
            _pacmanHealthCounter.UpdatePacmanHealthUI();
            StartCoroutine(RespawnPlayerAndGhost());       
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

    }

    public void TurnToWinScene()
    {
        Debug.Log("win scene");

    }

    public void TurnToLoseScene()
    {
        Debug.Log("lose scene");
    }

    private void StopAllGhost()
    {
        foreach (GameObject ghost in _ghosts)
        {
            ghost.GetComponent<Blinky_Pinky_ClydeMovementController>().GhostStop();
        }
    }
}
