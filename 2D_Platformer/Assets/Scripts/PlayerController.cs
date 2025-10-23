using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float jumpSpeed = 30.0f;
    [SerializeField]
    private GroundCheck groundCheck = null;

    private Rigidbody2D rigidBody = null;
    private PlayerInput playerInput = null;
    private InputAction moveAction = null;
    private InputAction jumpAction = null;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInput();
        moveAction = playerInput.Player.Move;
        jumpAction = playerInput.Player.Jump;

        jumpAction.performed += OnJump;
    }

    private void OnEnable()
    {
        playerInput.Enable();
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
        moveAction.Disable();
        jumpAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        rigidBody.linearVelocityX = moveInput.x * moveSpeed;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(groundCheck.IsGrounded && rigidBody.linearVelocityY <= 0.1f)
        {
            rigidBody.linearVelocityY = jumpSpeed;
        }
    }
}

