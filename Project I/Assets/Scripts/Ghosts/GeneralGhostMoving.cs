using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GhostState
{
    chase,
    scatter,
    frightened,
    eaten
}

public enum GhostName
{
    blinky,
    pinky,
    inky,
    clyde
}
public class GeneralGhostMoving : MonoBehaviour
{
    [Header("Ghost's Name")]
    

    [SerializeField] protected GhostName _ghostName;

    [Header("Ghost State")]
    [SerializeField] protected GhostState _currentGhostState;

    [Header("References")]
    [SerializeField] protected Transform _playerTransform;
                     protected NavMeshAgent _agent;

    [Header("Scatter Path References")]
    [SerializeField] protected Transform[] _scatterPath;
                     protected int _currentPoint;
                     protected Transform _currentGoal;


    // Start is called before the first frame update
    void Start()
    {
         _agent = this.GetComponent<NavMeshAgent>();
        //_currentGhostState = GhostState.chase;

        //set up start scatter point
        _currentPoint = 0;
        _currentGoal = _scatterPath[0];
            

    }

    // Update is called once per frame
    void Update()
    {
        if (CompareGhostState(GhostState.chase))
        {
            ChasePlayer();
        }else if (CompareGhostState(GhostState.scatter))
        {
            Scatter();
        }
    }

    protected virtual void ChasePlayer()
    {
        if (this._ghostName == GhostName.blinky)
        {

            _agent.SetDestination(_playerTransform.position);
        }
        if (this._ghostName == GhostName.pinky)
        {
            _agent.SetDestination(_playerTransform.position);
        }
        if (this._ghostName == GhostName.clyde)
        {

        }
    }

    //this method is for when ghost access "scatter mode"
    protected void Scatter()
    {
        _agent.SetDestination(_currentGoal.position);
    }

    // this method determines the next point in "scatter path"
    public void ChangeGoal()
    {
        if (_currentPoint ==_scatterPath.Length - 1)
        {
            _currentPoint = 0;
            _currentGoal = _scatterPath[0];
        }
        else
        {
            _currentPoint += 1;
            _currentGoal = _scatterPath[_currentPoint];
        }

    }

    //get ghost name
    public GhostName GetGhostName()
    {
        return this._ghostName;
    }

    //get current goal
    public Vector3 GetCurrentGoal()
    {
        return _currentGoal.position;
    }


    //get & set of ghost state
    private void ChangeGhostState(GhostState wantedGhostState)
    {
        this._currentGhostState = wantedGhostState;
    }

    public GhostState GetGhostState()
    {
        return this._currentGhostState;
    }

    //return if current ghost state is the state we want to compare
    public bool CompareGhostState(GhostState stateToCompare) {
        return GetGhostState() == stateToCompare;
    }
}

