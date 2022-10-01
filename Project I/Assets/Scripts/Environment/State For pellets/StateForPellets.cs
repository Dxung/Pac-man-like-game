using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//just a script to store references to player & ghosts' state controller
// because pellets cannot refers to a gameobject in scene, so we have to store this somewhere
public class StateForPellets : MonoBehaviour
{
    [SerializeField] private PlayerStateController _playerStateController;
    [SerializeField] private Blinky_Pinky_Inky_StateController[] _ghostStateController;

    public PlayerStateController GetPlayerStateController()
    {
        return _playerStateController;
    }

    public Blinky_Pinky_Inky_StateController[] GetGhostStateController()
    {
        return _ghostStateController;
    }



}
