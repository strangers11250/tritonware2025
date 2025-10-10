using UnityEngine;
using UnityEngine.InputSystem; // Required for new Input System

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Reference to the generated Input System class
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        // Create a new instance of the input actions
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        // Enable the action map
        inputActions.Player.Enable();

        // Subscribe to movement input events
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        // Unsubscribe and disable when not active
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
        inputActions.Player.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Apply movement to Rigidbody2D
        rb.linearVelocity = moveInput * moveSpeed;
    }

    // When Move input is active (stick or key pressed)
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // When Move input is released
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }
}