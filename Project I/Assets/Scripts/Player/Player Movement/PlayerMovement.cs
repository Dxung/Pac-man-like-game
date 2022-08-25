using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform _orientation;

    [Header("Input")]
    [SerializeField] private float _horizontalInput;
    [SerializeField] private float _verticalInput;

    private Vector3 _moveDirection;
    private Rigidbody _myRigidBody;

    [Header("Ground Check")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private bool _grounded;
    [SerializeField] private float _groundDrag;

    private void Start()
    {
        _myRigidBody = this.GetComponent<Rigidbody>();
        _myRigidBody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        _grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _whatIsGround);

        GetInput();
        SpeedControl();

        if (_grounded)
        {
            _myRigidBody.drag = _groundDrag;
        }
        else
        {
            _myRigidBody.drag = 0;
        }
    }

    private void GetInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    //Move the player follow which direction we are looking at
    private void MovePlayer()
    {
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;
        _myRigidBody.AddForce(_moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(_myRigidBody.velocity.x, 0f, _myRigidBody.velocity.z);


        //check if current speed is more than maxspeed
        //if yes, calculate max speed
        //apply calculated max speed to current speed
        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            _myRigidBody.velocity = new Vector3(limitedVelocity.x, _myRigidBody.velocity.y, limitedVelocity.z);
        }
    }


}
