using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private CharacterController playerController;
    public UIController uiController;

    [Header("Movemnt")]
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 2.5f;
    public Vector3 velocity;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius = 0.4f;
    public LayerMask groundMask;
    public bool isGounded;

    [Header("On Screen Controls")]
    public Joystick leftJoystick;
    public GameObject onScreenControls;
    public GameObject miniMap;


    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);
        
        if(isGounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal") + leftJoystick.Horizontal;
        float z = Input.GetAxis("Vertical") + leftJoystick.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;
        playerController.Move(move * maxSpeed * Time.deltaTime);

        if(Input.GetButton("Jump") && isGounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        // applies gravity
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            uiController.TakeDamage(5);
        }
    }

    public void OnJumpButtonPressed()
    {
        if (isGounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    public void OnMapButtonPressed()
    {
        miniMap.SetActive(!miniMap.activeInHierarchy);
    }
}
