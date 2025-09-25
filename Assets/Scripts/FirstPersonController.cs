using UnityEngine;
using UnityEngine.InputSystem;

// Script made with the assistance of ChatGPT due to time constraints. I had to migrate the project over from an older version of Unity to Unity 6.0, and needed to rebuild everything due to how differnetly the Input Action System worked.

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Look Settings")]
    public float mouseSensitivity = 1.0f;
    public Transform cameraTransform;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController controller;
    private PlayerInputActions inputActions;

    private Vector2 inputMove;
    private Vector2 inputLook;

    private Vector3 velocity;
    private bool isGrounded;

    private float xRotation = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => inputMove = Vector2.zero;

        inputActions.Player.Look.performed += ctx => inputLook = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => inputLook = Vector2.zero;

        inputActions.Player.Jump.performed += ctx => Jump();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
    }

    void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector3 move = transform.right * inputMove.x + transform.forward * inputMove.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleLook()
    {
        float mouseX = inputLook.x * mouseSensitivity;
        float mouseY = inputLook.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
