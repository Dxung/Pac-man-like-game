using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ghost State Timer", menuName = "My Game/ Ghost State Timer")]
public class StateModeTime : ScriptableObject
{

    [SerializeField] private int _frightenedModeTime;

    //scatter1 - chase1- scatter2- chase2 - scatter3- chase3 - scatter 4
    [SerializeField] private int[] _scatterModeTime;
    [SerializeField] private int[] _chaseModeTime;
    
    public int GetScatterModeTimeAt(int number)
    {
        return _scatterModeTime[number-1];
    }

    public int GetChaseModeTimeAt (int number)
    {
        return _chaseModeTime[number-1];
    }

    public int GetFrightenedModeTime()
    {
        return _frightenedModeTime;
    }
}
