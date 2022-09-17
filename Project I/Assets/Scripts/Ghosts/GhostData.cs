using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script contains data about a ghost and methods for controller
// + name
// + scatterPath
// + state

//the scatter path is empty at the first time run game
//needs to make "prefabs" of "scatter path points" and add all that prefabs in the appropriate "game object array" in "scatter point controller"


[CreateAssetMenu(fileName = "Ghost Data", menuName = "My Game/ Ghost Data")]
public class GhostData : ScriptableObject
{
    [Header("Ghost's Name")]
    [SerializeField] private GhostName _ghostName;


    [Header("Scatter Path References")]
    [SerializeField] private List<Transform> _scatterPath;
    private int _currentScatterPoint;
    private Transform _currentScatterGoal;

    [Header("Ghost State")]
    [SerializeField] private GhostState _currentGhostState;
    [SerializeField] private GhostState _previousGhostState;



    //METHOD FOR GHOST'S NAME:

    public bool CompareGhostName(GhostName ghostNameToCompare)
    {
        return GetGhostName() == ghostNameToCompare;
    }


    //METHODS FOR GHOST'S SCATTER-PATH:

    public void ChangeGoal()
    {
        if (_currentScatterPoint == _scatterPath.Count - 1)
        {
            _currentScatterPoint = 0;
            _currentScatterGoal = _scatterPath[0];
        }
        else
        {
            _currentScatterPoint += 1;
            _currentScatterGoal = _scatterPath[_currentScatterPoint];
        }
    }

    public bool CompareCurrentGoal(Vector3 positionToCompare)
    {
        return GetCurrentGoal() == positionToCompare;
    }

    //this method is used for reseting "current point to the first point in path" and "current goal to that position".
    //use when first time instantiate ghost
    //use after ghost get out of scatter mode
    public void ResetScatterPoint()
    {
        _currentScatterPoint = 0;
        _currentScatterGoal = _scatterPath[0];
    }


    //METHODS FOR GHOST'S STATE:

    //reset ghost state to "chase"
    //use for first time instantiate ghost
    //use after ghost finish its scatter mode
    public void ResetGhostState()
    {
        SetGhostState(GhostState.chase);
    }

    public bool CompareGhostState(GhostState stateToCompare)
    {
        return GetGhostState() == stateToCompare;
    }


    //GETTER and SETTER:

    //Ghost's name:
    private GhostName GetGhostName()
    {
        return this._ghostName;
    }

    //Scatter Path:
    public Vector3 GetCurrentGoal()
    {
        return this._currentScatterGoal.position;
    }

    //Ghost's State:
    private void SetGhostState(GhostState wantedGhostState)
    {
        this._currentGhostState = wantedGhostState;
    }

    private GhostState GetGhostState()
    {
        return this._currentGhostState;
    }

    //Add transform of the scatter point prefab to the scatterpath array
    public void AddScatterPointToPath(GameObject[] prefabScatterPoint)
    {
        foreach(GameObject scatterPoint in prefabScatterPoint)
        {
            _scatterPath.Add(scatterPoint.transform);
        }

        Debug.Log(_ghostName+": "+_scatterPath.Count);
        foreach(Transform testets in _scatterPath)
        {
            Debug.Log(testets);
        }
        
    }

}
