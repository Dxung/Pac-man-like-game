using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//when a ghost touch a scatter point, this script will change the next destination point
public class TriggerForGhostGoal : MonoBehaviour
{
    [SerializeField] private GhostName _triggerForWhichGhost;

    private void OnTriggerEnter(Collider other)
    {
 
        //get the component of the ghost touching the trigger
        Blinky_Pinky_ClydeMovementController ghostMovementController = other.gameObject.GetComponent<Blinky_Pinky_ClydeMovementController>();
        Blinky_Pinky_Inky_StateController ghostStateController = other.gameObject.GetComponentInChildren<Blinky_Pinky_Inky_StateController>();

        //check if this point is the current goal point
        if (ghostMovementController.CompareCurrentGoal(this.transform.position)) 
        {

            //only change goal when ghost in scatter mode
            if (ghostStateController.CheckCurrentState(GhostState.scatter))
            {
                
             //check if this ghost  has the same "Ghost Name" with the one set for this point
                if (ghostMovementController.IsItThisGhost(_triggerForWhichGhost))
                {
                    ghostMovementController.ChangeGoal();
                }
            }
        }
    }
}
