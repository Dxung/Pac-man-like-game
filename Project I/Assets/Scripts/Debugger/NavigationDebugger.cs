using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Renderer))]
public class NavigationDebugger : MonoBehaviour
{

    [SerializeField] private NavMeshAgent _ghostsToDebug;

    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (_ghostsToDebug.hasPath)
        {
            //vertices of Renderer ~ VOR
            //Corner Point of "navmesh Ghosts moving path" ~ CP

            //set the number of VOR with number of CP
            _lineRenderer.positionCount = _ghostsToDebug.path.corners.Length;

            //set the position of VOR with position of CP
            _lineRenderer.SetPositions(_ghostsToDebug.path.corners);

            _lineRenderer.enabled = true;
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}
