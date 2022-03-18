using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player Camera")]
    //public float mouseSensitivity = 10.0f;
    public float controlSensitivity;
    public Transform playerBody;

    public Joystick rightJoystick;

    private float XRotation = 0.0f;
    
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        controlSensitivity = 3f;
    }

    void Update()
    {
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        float horizontal = rightJoystick.Horizontal * controlSensitivity;
        float vertical = rightJoystick.Vertical * controlSensitivity;

        XRotation -= vertical;
        XRotation = Mathf.Clamp(XRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * horizontal);
    }
}