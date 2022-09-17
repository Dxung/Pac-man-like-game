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
        GhostMovementController ghostController = other.gameObject.GetComponent<GhostMovementController>();

        //check if this point is the current goal point
        if (ghostController.CompareCurrentGoal(this.transform.position)) 
        {

            //only change goal when ghost in scatter mode
            if (ghostController.CompareCurrentState(GhostState.scatter))
            {
                
             //check if this ghost  has the same "Ghost Name" with the one set for this point
                if (ghostController.CompareGhostName(_triggerForWhichGhost))
                {
                    ghostController.ChangeScatterGoal();
                }
            }
        }
    }
}
