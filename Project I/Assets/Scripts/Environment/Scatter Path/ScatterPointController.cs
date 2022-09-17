using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterPointController : MonoBehaviour
{
    [SerializeField] private GameObject[] _blinkyScatterPoint;
    [SerializeField] private GameObject[] _pinkyScatterPoint;
    [SerializeField] private GameObject[] _inkyScatterPoint;
    [SerializeField] private GameObject[] _clydeScatterPoint;

    [SerializeField] private GhostData _blinkyData;
    [SerializeField] private GhostData _pinkyData;
    [SerializeField] private GhostData _inkyData;
    [SerializeField] private GhostData _clydeData;


    private void Awake()
    {
            CreatePrefabs(_blinkyScatterPoint);
            CreatePrefabs(_pinkyScatterPoint);
            CreatePrefabs(_inkyScatterPoint);
            CreatePrefabs(_clydeScatterPoint);

        CreateScatterPathForGhostData(_blinkyScatterPoint, _blinkyData);
        CreateScatterPathForGhostData(_pinkyScatterPoint, _pinkyData);
        CreateScatterPathForGhostData(_inkyScatterPoint, _inkyData);
        CreateScatterPathForGhostData(_clydeScatterPoint, _clydeData);

    }

    //Create prefabs of all the "scatterPoint gameobject" in array
    private void CreatePrefabs(GameObject[] scatterPointArrayToCreate)
    {
        foreach (GameObject scatterPointObject in scatterPointArrayToCreate)
        {
            Instantiate(scatterPointObject);
        }
    }

    //Add Transform of all gameobjects in the array to its appropriate "ghost data"
    private void CreateScatterPathForGhostData(GameObject[] scatterPointArray, GhostData ghostData)
    {
        ghostData.AddScatterPointToPath(scatterPointArray);
    }



}
