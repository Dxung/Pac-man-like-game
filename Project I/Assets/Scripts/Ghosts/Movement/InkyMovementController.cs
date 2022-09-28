using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyMovementController : Blinky_Pinky_ClydeMovementController
{

    [Header("Inky References")]
    [SerializeField] private GameObject _otherGhosts;
    private Vector3 _pos;

    protected override void Chase()
    {
        if (_ghostData.CompareGhostName(GhostName.inky))
        {
            SetGhostSpeed(_ghostSpeed);
            _pos = transform.position + (_playerTransform.position - _otherGhosts.transform.position) * 1.5f;
            _agent.SetDestination(_pos);
        }
    }

}
