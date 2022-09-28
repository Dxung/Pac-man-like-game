using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{

    public bool _canMove { get; private set; } = true;

    [Header("Movement Parameters")]
    [SerializeField] private float _constantSpeed = 2f;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _gravity = 9.8f;

    [Header("Look Parameters")]
    //X and Y is the axis to turn the camera rotation.
    //X : for looking up and down
    //Y : for looking left and right
    [SerializeField, Range(1, 10)] private float _lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float _lookSpeedY = 2.0f;

    //this is for the max and min degree that we can rotate the camera
    //"_upperLookLimit" : the maximum degree we can look up
    //"_lowerLookLimit" : the maxmimum degree we can look down
    [SerializeField, Range(1, 180)] private float _upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float _lowerLookLimit = 80.0f;

    [Header("Player State")]
    private PlayerStateController _playerStateController;

    [Header("References")]
    private Camera _playerCamera;
    private CharacterController _myCharacterController;

    //"_currentInput"  : to store the input received from the keyboard
    //"_moveDirection" : after calculate everything, we will apply this value to "CharacterController" to move player
    private Vector3 _moveDirection;
    private Vector2 _currentInput;

    //this is the value we will use to clamp ( <min --> = min; >max --> =max;) with out upper and lower look limit
    // we just need a reference here, not a temp variable.
    private float _rotationX = 0;

    [SerializeField] private Vector3 _startingposition;


    private void Start()
    {
        ReSpawnPacman();
    }

    // Start is called before the first frame update
    void Awake()
    {
        _playerStateController = this.gameObject.GetComponentInChildren<PlayerStateController>();
        _playerCamera = GetComponentInChildren<Camera>();
        _myCharacterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        SpeedControl();
        if (_canMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            ApplyFinalMovement();
        }
        
    }


    private void HandleMovementInput()
    {
        //get the input and multiply with speed.
        //the "get axis()" will return float value between -1 and 1 on the 2 dimensional space
        //_currentInput.x : Vertical (forward/backward)
        //_currentInput.y : Horizontal (left and right) 
        Vector2 tempToNormalize = new Vector2(Input.GetAxis("Vertical"),Input.GetAxis("Horizontal"));
        _currentInput = tempToNormalize.normalized*_walkSpeed;
        float moveDirectionY = _moveDirection.y;

        /* The "_current input" already contains values that [how long the player will move with its local direction]
         * Multiplying that value with "transform.TransformDirection (Vector3.forward/right)" just to turns local direction to world direction for calculating only.
         * ==> To make sure the player moves with the same "local direction" that "world distance"
        */

        //For example: if the player is facing NW (NorthWest), when we press W or forward arrow, the player will keep going NW

        /* [the direction player moves * the distance player moves]
         * "transform.TransformDirection(Vector3.forward)*_currentInput.x" : forward/backward * distance
         * "transform.TransformDirection(Vector3.right) * _currentInput.y" " left/right * distance
        */
        _moveDirection = (transform.TransformDirection(Vector3.forward) * _currentInput.x) + (transform.TransformDirection(Vector3.right) * _currentInput.y);

        //To reset player's height to its orginal value before calculating
        _moveDirection.y = moveDirectionY;



    }

    private void HandleMouseLook()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _lookSpeedY;
        _rotationX = Mathf.Clamp(_rotationX, -_lowerLookLimit, _upperLookLimit);

        //we use "localRotation", so rotation of the camera will be relative to rotation of player
        //so the degree camera rotate will be calculated through player position, instead of world position
        //"look to the left, bro!" It is your left, not the left of the world
        _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);

        //with this, when camera looks left/right, player's body will look left or right too
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeedX, 0);
    }

    private void ApplyFinalMovement()
    {
        //if player is not on the ground, use gravity to slowly pull player down
        if(!_myCharacterController.isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }

        //move player with the value "_moveDirection" we calculated above [ HandleMovementInput() and gravity pull ]
        _myCharacterController.Move(_moveDirection * Time.deltaTime);

    }

    ///Speed Control:
    private void SpeedControl()
    {
        if (_playerStateController.CheckCurrentState(PlayerState.normal))
        {
            SpeedChange(_constantSpeed);
        }
        else if (_playerStateController.CheckCurrentState(PlayerState.consume))
        {
            SpeedChange(0.9f * _constantSpeed);
        }
        else if (_playerStateController.CheckCurrentState(PlayerState.powerUp))
        {
            SpeedChange(1.5f * _constantSpeed);
        }else if (_playerStateController.CheckCurrentState(PlayerState.dead))
        {
            SpeedChange(0);
        }
    }

    private void SpeedChange(int speed)
    {
        _walkSpeed = speed;
    }

    private void SpeedChange(float speed)
    {
        _walkSpeed = speed;
    }

    public void ReSpawnPacman()
    {
        _canMove = false;
        this.gameObject.GetComponent<CharacterController>().enabled = false;
        this.gameObject.transform.position = _startingposition;
        this.gameObject.GetComponent<CharacterController>().enabled = true;
        _canMove = true;
        SpeedChange(_constantSpeed);
        _playerStateController.ReSpawn();
    }

}
