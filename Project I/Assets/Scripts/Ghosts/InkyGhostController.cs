using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyGhostController : GhostMovementController
{

    [Header("Inky References")]
    [SerializeField] private GameObject _otherGhosts;
    private Vector3 _pos;

    protected override void ChasePlayer()
    {
        base.ChasePlayer();
        if (_ghostData.CompareGhostName(GhostName.inky))
        {
            _pos = transform.position + (_playerTransform.position - _otherGhosts.transform.position) * 1.5f;
            _agent.SetDestination(_pos);
        }
    }

}