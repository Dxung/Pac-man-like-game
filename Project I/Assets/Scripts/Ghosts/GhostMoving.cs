using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMoving : MonoBehaviour
{
    [Header("Ghost's Name")]
    [SerializeField] private bool blinky = false;
    [SerializeField] private bool pinky = false;
    [SerializeField] private bool inky = false;
    [SerializeField] private bool clyde = false;

    [Header("References")]
    [SerializeField] private Transform _playerTransform;
                     private NavMeshAgent _agent;


    // Start is called before the first frame update
    void Start()
    {
         _agent = this.GetComponent<NavMeshAgent>();
        Debug.Log(_agent);
        
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();

    }

    private void ChasePlayer()
    {
        if (blinky)
        {
            Debug.Log(_playerTransform);
            _agent.SetDestination(_playerTransform.position);
        }
        if (pinky)
        {

        }
        if (inky)
        {

        }
        if (clyde)
        {

        }
    }

    private void Scatter()
    {

    }
}
