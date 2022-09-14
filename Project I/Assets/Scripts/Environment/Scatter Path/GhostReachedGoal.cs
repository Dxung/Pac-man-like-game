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
        GeneralGhostMoving ghostScript = other.gameObject.GetComponent<GeneralGhostMoving>();

        //check if this point is the current goal point
        if (this.transform.position == ghostScript.GetCurrentGoal())
        {

            Debug.Log(this.transform.position == ghostScript.GetCurrentGoal());

            //only change goal when ghost in scatter mode
            if (ghostScript.GetGhostState() == GhostState.scatter)
            {
                
                GhostName nameOfGhostThatTouch = ghostScript.GetGhostName();

                //check if this ghost  has the same "Ghost Name" with the one set for this point
                if (nameOfGhostThatTouch == _triggerForWhichGhost)
                {
                    ghostScript.ChangeGoal();
                }
            }
        }
    }
}
