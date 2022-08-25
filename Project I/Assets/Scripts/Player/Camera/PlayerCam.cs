using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("sensitive of camera Rotation")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [Header("orientation")]
    [SerializeField] private Transform _orientation;

    [SerializeField] private float xRotation;
    [SerializeField] private float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //In unity, this is way calculate controlling rotation?
        yRotation += mouseX;
        xRotation -= mouseY;

        //this is to make sure character cannot look up or down more than 90 degree. 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        //this code is for rotating the camera which is on both X axis and Y axis (up-down and left-right)
        //while the character only rotating Y axis (left-right only) - Because we do not use camera for out head but whole object instead, so rotate on X axis will make our character to lie in the air .
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        _orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }



}
