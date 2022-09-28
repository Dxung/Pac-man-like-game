using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrightenedEffect : MonoBehaviour
{
    [SerializeField] protected GameObject[] _ghosts;
    [SerializeField] protected Transform _pacmanTransform;

    [SerializeField] protected bool _isSoundOn;


    protected void Start()
    {
        _isSoundOn = false;
    }

    protected void Update()
    {
        Transform bestGhost = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (GameObject potentialGhost in _ghosts)
        {
            if (CheckIfGhostNeedsSiren(potentialGhost))
            {

                Vector3 directionToGhost = potentialGhost.transform.position - _pacmanTransform.position;
                float dSqrToGhost = directionToGhost.sqrMagnitude;
                if (dSqrToGhost < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToGhost;
                    bestGhost = potentialGhost.transform;
                }
            }
        }

        if (bestGhost != null)
        {
            transform.position = bestGhost.position;
            FoundSuitableGhost();
        }
        else
        {
            NotFoundSuitableGhost();
        }
    }

    protected virtual bool CheckIfGhostNeedsSiren(GameObject ghost)
    {
        Blinky_Pinky_Inky_StateController ghostStateController = ghost.GetComponentInChildren<Blinky_Pinky_Inky_StateController>();
        return ghostStateController.CheckCurrentState(GhostState.frightened);
    }

    protected virtual void FoundSuitableGhost()
    {
        if (!_isSoundOn)
        {
            this.gameObject.GetComponent<AudioManager>().Play("ghost frightened");
            _isSoundOn = true;
        }
    }

    protected virtual void NotFoundSuitableGhost()
    {
        if (_isSoundOn)
        {
            this.gameObject.GetComponent<AudioManager>().Stop("ghost frightened");
            _isSoundOn = false;
        }
    }
}
