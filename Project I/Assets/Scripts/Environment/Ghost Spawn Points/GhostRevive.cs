using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRevive : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ghosts"))
        {
            Blinky_Pinky_Inky_StateController ghostStateController = other.gameObject.GetComponentInChildren<Blinky_Pinky_Inky_StateController>();
            if (ghostStateController.CheckCurrentState(GhostState.eaten))
            {
                ghostStateController.FinishEatenState();
            }
        }
    }
}
