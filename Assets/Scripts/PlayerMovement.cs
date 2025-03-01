using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float sprintSpeed = 10f;
    public float crouchSpeed = 3f;
    private float currentSpeed;
    private CharacterController controller;

    [Header("Jumping & Vaulting")]
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;
    
    [Header("Player Model Visibility")]
    public bool showPlayerModel = false; // Toggle in Inspector
    private GameObject playerModel;

    private Transform cam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;

        // Find the player model (Robot Kyle)
        playerModel = transform.Find("RobotKyle")?.gameObject;

        if (playerModel != null)
        {
            // Set visibility based on Inspector toggle
            playerModel.SetActive(showPlayerModel);
        }
    }

    void Update()
    {
        // Toggle player model visibility at runtime
        if (playerModel != null)
        {
            playerModel.SetActive(showPlayerModel);
        }

        // Ground Check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, LayerMask.GetMask("Ground"));
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Speed Control
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        if (Input.GetKey(KeyCode.LeftControl)) currentSpeed = crouchSpeed;

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
