using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPS_Behaviour : MonoBehaviour
{
    [Header("External Objects")]
    public Camera playerCamera;
    CharacterController characterController;
    public AudioSource jumpSound;

    [Header("Movement")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;

    [Range(1.0f, 10.0f)]
    public float jumpHeight = 8.0f;
    [Space]
    [Range(1.0f, 10.0f)]
    public float lookSpeed = 2.0f;
    [Range(10f, 90f)]
    [Tooltip("How low/High can the Player Look?")]
    public float lookXLimit = 45.0f;
    
    public float gravity = 20.0f;
    [Space]
    public Quaternion characterView;

    public SkinnedMeshRenderer[] mesh;

    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    public float rotationX = 0;
    [HideInInspector]
    public bool canMove = true;
    bool isRunning = false;
    [HideInInspector]
    public bool isJumping = false;


    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKey("left shift"))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpHeight;
            jumpSound.Play();
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            playerCamera = GetComponentInChildren<Camera>();
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            characterView = transform.rotation;
        }
    }

    private void FixedUpdate()
    {
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            isJumping = true;
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }

        if (transform.position.y <= -5f)
        {
            PauseMenu.Instance.ShowPaused(true);
        }
    }
}
