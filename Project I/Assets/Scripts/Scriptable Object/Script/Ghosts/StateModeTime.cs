using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State Timer", menuName = "My Game/ State Timer")]
public class StateModeTime : ScriptableObject
{
    [Header("Ghost and Player")]
    [SerializeField] private int _frightenedPowerUpTimer;

    [Header("Ghost")]
    //scatter1 - chase1- scatter2- chase2 - scatter3- chase3 - scatter 4
    [SerializeField] private int[] _scatterModeTime;
    [SerializeField] private int[] _chaseModeTime;

    [Header("Player")]
    [SerializeField] private float _consumeTime;

    public int GetScatterModeTimeAt(int number)
    {
        return _scatterModeTime[number-1];
    }

    public int GetChaseModeTimeAt (int number)
    {
        return _chaseModeTime[number-1];
    }

    public int GetFrightenedPowerUpTime()
    {
        return _frightenedPowerUpTimer;
    }

    public float GetConsumeTime()
    {
        return _consumeTime;
    }
}
