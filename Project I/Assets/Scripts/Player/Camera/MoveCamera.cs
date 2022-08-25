using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Script is for making the camera always move with my player
public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    private void Update()
    {
        transform.position = _cameraPosition.position;
    }
}
