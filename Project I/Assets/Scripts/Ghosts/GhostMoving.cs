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

    [Header("Inky References")]
    [SerializeField] private GameObject _otherGhosts;
                     private Vector3 _pos;



    // Start is called before the first frame update
    void Start()
    {
         _agent = this.GetComponent<NavMeshAgent>();
        
        
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
            
            _agent.SetDestination(_playerTransform.position);
        }
        if (pinky)
        {

        }
        if (inky)
        {
            _pos = transform.position + (_playerTransform.position - _otherGhosts.transform.position) * 1.5f;
            _agent.SetDestination(_pos);
        }
        if (clyde)
        {
            
        }
    }

    private void Scatter()
    {

    }
}
