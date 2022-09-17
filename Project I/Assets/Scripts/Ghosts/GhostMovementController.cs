using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public enum GhostName
{
    blinky,
    pinky,
    inky,
    clyde
}


public class GhostMovementController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform _playerTransform;
                     protected NavMeshAgent _agent;


    [Header("Ghost Data")]
    [SerializeField] protected GhostData _ghostData;

    // Start is called before the first frame update
    void Start()
    {
         _agent = this.GetComponent<NavMeshAgent>();

        //set up start scatter point
        _ghostData.ResetScatterPoint();
            

    }

    // Update is called once per frame
    void Update()
    {
        if (_ghostData.CompareGhostState(GhostState.chase))
        {
            ChasePlayer();
        }else if (_ghostData.CompareGhostState(GhostState.scatter))
        {
            Scatter();
        }
    }

    protected virtual void ChasePlayer()
    {
        if (_ghostData.CompareGhostName(GhostName.blinky))
        {

            _agent.SetDestination(_playerTransform.position);
        }
        if (_ghostData.CompareGhostName(GhostName.pinky))
        {
            _agent.SetDestination(_playerTransform.position);
        }
        if (_ghostData.CompareGhostName(GhostName.clyde))
        {

        }
    }

    //this method is for when ghost access "scatter mode"
    //ghost will go to the currentGoal position
    protected void Scatter()
    {
        _agent.SetDestination(_ghostData.GetCurrentGoal());
    }


    //compare methods:
    public bool CompareCurrentGoal(Vector3 positionToCompare)
    {
        return _ghostData.CompareCurrentGoal(positionToCompare);
    }

    public bool CompareCurrentState(GhostState stateToCompare)
    {
        return _ghostData.CompareGhostState(stateToCompare);
    }

    public bool CompareGhostName(GhostName ghostName)
    {
        return _ghostData.CompareGhostName(ghostName);
    }

    //change Goal:
    public void ChangeScatterGoal()
    {
        _ghostData.ChangeGoal();
    }
}

