using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrightenedEffect : GhostSirenEffect
{
    protected override bool CheckIfGhostNeedsSiren(GameObject ghost)
    {
        Blinky_Pinky_Inky_StateController ghostStateController = ghost.GetComponentInChildren<Blinky_Pinky_Inky_StateController>();
        return ghostStateController.CheckCurrentState(GhostState.frightened);
    }

    protected override void FoundSuitableGhost()
    {
        if (!_isSoundOn)
        {
            this.gameObject.GetComponent<AudioManager>().Play("ghost frightened");
            _isSoundOn = true;
        }
    }

    protected override void NotFoundSuitableGhost()
    {
        if (_isSoundOn)
        {
            this.gameObject.GetComponent<AudioManager>().Stop("ghost frightened");
            _isSoundOn = false;
        }
    }
}
