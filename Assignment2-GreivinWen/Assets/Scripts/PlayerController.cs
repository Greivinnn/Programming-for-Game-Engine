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

    [SerializeField]
    private Animator animator = null;

    private Rigidbody2D rigidBody = null;
    private PlayerInput playerInput = null;
    private InputAction moveAction = null;
    private InputAction jumpAction = null;
    private float direction = 1.0f;
    private bool isJumping = false;
    private bool isFalling = false;
    private Vector3 startingPosition = Vector3.zero;
    private Vector3 lastCheckpoint = Vector3.zero;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInput = new PlayerInput();
        if (gameObject.name.Contains("FrogNinja"))
        {
            moveAction = playerInput.Player.Move;
            jumpAction = playerInput.Player.Jump;
        }
        else if (gameObject.name.Contains("AstroBoy"))
        {
            moveAction = playerInput.Player2.Move;
            jumpAction = playerInput.Player2.Jump;
        }

            jumpAction.performed += OnJump;

        startingPosition = transform.position;
        lastCheckpoint = startingPosition;
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

        if (Mathf.Abs(moveInput.x) > 0.1f)
        {
            direction = (moveInput.x > 0.0f) ? 1.0f : -1.0f;
        }
        float speed = Mathf.Abs(rigidBody.linearVelocityX);

        if (!groundCheck.IsGrounded && rigidBody.linearVelocityY <= 0.1f)
        {
            isFalling = true;
            isJumping = false;
        }
        else if(groundCheck.IsGrounded && !isJumping)
        {
            isFalling = false;
        }

        animator.SetFloat("Speed", speed);
        animator.SetFloat("Direction", direction);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsFalling", isFalling);

        if (isFalling && transform.position.y < -100.0f)
        {
            Die();
        }
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;

        // Apply checkpoint to both players
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        foreach (var p in players)
        {
            p.lastCheckpoint = checkpointPosition;
        }
        Debug.Log($"Shared checkpoint set to {lastCheckpoint}");
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (groundCheck.IsGrounded && rigidBody.linearVelocityY <= 0.1f)
        {
            rigidBody.linearVelocityY = jumpSpeed;
            isJumping = true;
            isFalling = false;
        }
    }

    public void Die()
    {
        Debug.Log("Player died — resetting both players.");

        // Find both players
        // FindObjectsOfType<PlayerController>(); is slow but acceptable here since death is infrequent
        // this returns a array of the list of players in the scene
        // do not recommend using this in Update() or frequently called methods
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        foreach (var p in players)
        {
            p.rigidBody.linearVelocity = Vector2.zero;
            p.transform.position = lastCheckpoint;
            p.isJumping = false;
            p.isFalling = false;
            p.animator.SetBool("IsJumping", false);
            p.animator.SetBool("IsFalling", false);
        }
    }
}

