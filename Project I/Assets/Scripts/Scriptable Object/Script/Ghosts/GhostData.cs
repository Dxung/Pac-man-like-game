using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script contains data about a ghost and methods for controller
// + name
// + scatterPath

//the scatter path is empty at the first time run game
//needs to make "prefabs" of "scatter path points" and add all that prefabs in the appropriate "game object array" in "scatter point controller"


[CreateAssetMenu(fileName = "Ghost Data", menuName = "My Game/ Ghost Data")]
public class GhostData : ScriptableObject
{
    [Header("Ghost's Name")]
    [SerializeField] private GhostName _ghostName;

    [Header("Ghost's Spawn Position")]
    [SerializeField] private Vector3 _spawnPosition;


    [Header("Scatter Path References")]
    [SerializeField] private List<Vector3> _scatterPath;


                                                ///GHOST'S NAME:

    public bool CompareGhostName(GhostName ghostNameToCompare)
    {
        return GetGhostName() == ghostNameToCompare;
    }

    public GhostName GetGhostName()
    {
        return this._ghostName;
    }

                                                ///Spawn's Position
    public Vector3 GetSpawnPosition()
    {
        return _spawnPosition;
    }

                                                ///SCATTER PATH:
    //get scatter point transform at 
    public Vector3 GetScatterPointAtNumber(int number)
    {
        return _scatterPath[number];
    }
    
    public int GetNumberOfScatterPoint()
    {
        return _scatterPath.Count;
    }

    //Add transform of the scatter point prefab to the scatterpath array
    public void AddScatterPointToPath(GameObject[] prefabScatterPoint)
    {
        _scatterPath.Clear();
        foreach(GameObject scatterPoint in prefabScatterPoint)
        {           
            _scatterPath.Add(scatterPoint.transform.position);
            
        }

    }
    public void AddSpawnPoint(GameObject prefabSpawnPoint)
    {
        _spawnPosition = prefabSpawnPoint.transform.position;
    }
}
