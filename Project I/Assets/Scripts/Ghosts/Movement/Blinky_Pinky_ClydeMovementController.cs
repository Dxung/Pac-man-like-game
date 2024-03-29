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

public class Blinky_Pinky_ClydeMovementController : MonoBehaviour
{
    [Header("Ghost Data")]
    [SerializeField] protected GhostData _ghostData;


    [Header("Navigation Path Finding")]
    [SerializeField] protected Transform _playerTransform;
    protected NavMeshAgent _agent;

    [Header("Scatter Path Finding")]
    [SerializeField] protected int _currentScatterPoint;
    [SerializeField] protected Vector3 _currentScatterGoal;

    [Header("Ghost State Controller")]
    protected Blinky_Pinky_Inky_StateController _ghostStateController;

    [Header("ghost speed")]
    [SerializeField] protected float _ghostSpeed;
    [SerializeField] protected bool _ghostStop;

    [SerializeField] public bool _click;

    protected void Start()
    {
        _ghostStateController = this.gameObject.GetComponentInChildren<Blinky_Pinky_Inky_StateController>();
        _agent = this.GetComponent<NavMeshAgent>();

        //Spawn Ghost
        this.transform.position = _ghostData.GetSpawnPosition();
        ResetScatterPoint();

        //ghost speed
        _ghostStop = false;
        SetupStartingSpeed();


    }


    protected void Update()
    {
        UpdateMovement();

        if (_click)
        {
            this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            this.gameObject.transform.position = Vector3.zero;
            this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    protected void LateUpdate()
    {
        LookAtMovingDirection();
    }

    protected virtual void UpdateMovement()
    {
        if (!_ghostStop)
        {
            if (_ghostStateController.CheckCurrentState(GhostState.chase))
            {

                if (!CompareCurrentScatterPoint(0))
                {
                    ResetScatterPoint();
                }
                Chase();
            }
            if (_ghostStateController.CheckCurrentState(GhostState.scatter))
            {
                Scatter();
            }
            if (_ghostStateController.CheckCurrentState(GhostState.frightened))
            {
                Frightened();
            }
            if (_ghostStateController.CheckCurrentState(GhostState.eaten))
            {
                Eaten();
            }
        }
    }
    
    ///ReSpawn
    
    //teleport ghosts back
    // reset scatter point
    //turn on navmesh agent again
    //start updating movement for state again
    public void RespawnGhostMovement()
    {
        this.gameObject.transform.position = _ghostData.GetSpawnPosition();
        ResetScatterPoint();
        this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        _ghostStop = false;
    }

    //not update speed for state
    //disable navmesh agent
    public void GhostStop()
    {
        _ghostStop = true;
        _agent.SetDestination(this.gameObject.transform.position);
        SetGhostSpeed(0);
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;

    }

    ///Movement:
    protected virtual void Chase()
    {
        SetGhostSpeed(_ghostSpeed);
        _agent.SetDestination(_playerTransform.position);
    }

    protected void Scatter()
    {
        SetGhostSpeed(_ghostSpeed);
        _agent.SetDestination(GetCurrentScatterGoal());
    }

    protected void Frightened()
    {
         SetPerCentOfGhostSpeed(0.5f);
        _agent.SetDestination(_ghostData.GetSpawnPosition());
    }

    protected void Eaten()
    {
         SetPerCentOfGhostSpeed(1.5f);
        _agent.SetDestination(_ghostData.GetSpawnPosition());
    }




    ///Speed of Navigation Path Agent
    
    protected void SetupStartingSpeed()
    {
        if (_ghostData.CompareGhostName(GhostName.pinky))
        {
            _ghostSpeed = 1.17f;
        }
        else
        {
            _ghostSpeed = 1.62f;
        }

        SetGhostSpeed(_ghostSpeed);
    }

    private void SetPerCentOfGhostSpeed(float percentOfTheNewSpeed)
    {
        float temp = _ghostSpeed * percentOfTheNewSpeed;
        SetGhostSpeed(temp);
        
    }

    protected void SetGhostSpeed(float speed)
    {
        _agent.speed = speed;
    }



    ///Scatter Path                                            

    //Use for reseting "current point to the first point in path" and "current goal to that position".
    //use when first time instantiate ghost
    //use after ghost get out of scatter mode to chase mode
    //Dont use when ghost change to frightened/eaten mode --> because: the previous state maybe "scatter", it will come back to its current value to continue state movement
    private void ResetScatterPoint()
    {
        _currentScatterPoint = 0;
        _currentScatterGoal = _ghostData.GetScatterPointAtNumber(0);
    }

    //Used by "scatter point's trigger" to tell ghost to go to the next point
    public void ChangeGoal()
    {
        if (CompareCurrentScatterPoint(_ghostData.GetNumberOfScatterPoint() - 1))
        {
            SetCurrentScatterPoint(0);
            SetCurrentScatterGoal(_ghostData.GetScatterPointAtNumber(0));
        }
        else
        {
            ToNextScatterPoint();
            SetCurrentScatterGoal(_ghostData.GetScatterPointAtNumber(GetCurrentScatterPoint()));
        }
    }

    ///Ghost's Name
    public bool IsItThisGhost(GhostName name)
    {
        return _ghostData.CompareGhostName(name);
    }

    ///For Ghosts' Rotation: Ghost Looks at the direction its going

    protected void LookAtMovingDirection()
    {
        if (_agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            this.gameObject.transform.rotation = Quaternion.LookRotation(_agent.velocity.normalized);
        }
    }

    ///Getter And Setter

    private int GetCurrentScatterPoint()
    {
        return _currentScatterPoint;
    }
    private void SetCurrentScatterPoint(int value)
    {
        this._currentScatterPoint = value;
    }
    private bool CompareCurrentScatterPoint(int value)
    {
        return _currentScatterPoint == value;
    }
    private void ToNextScatterPoint()
    {
        _currentScatterPoint++;
    }


    private Vector3 GetCurrentScatterGoal()
    {
        return _currentScatterGoal;
    }
    private void SetCurrentScatterGoal(Vector3 value)
    {
        _currentScatterGoal = value;
    }
    public bool CompareCurrentGoal(Vector3 position)
    {
        return GetCurrentScatterGoal() == position;
    }


}

