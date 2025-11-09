using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;
    [SerializeField]
    private float jumpSpeed = 1.0f;
    [SerializeField]
    private GroundCheck groundCheck = null;
    [SerializeField]
    private Transform lookTarget = null;
    [SerializeField]
    private float lookSensitivity = 75.0f;
    [SerializeField]
    private float lookDeadZone = 0.1f;
    [SerializeField]
    private float maxLookUp = 70.0f;
    [SerializeField]
    private float minLookDown = -70.0f;
    [SerializeField]
    private bool invertY = false;
    [SerializeField]
    Animator animator = null;
    [SerializeField]
    private float checkPointOffset = 4.0f;

    private Rigidbody rigidBody = null;
    private PlayerInput input = null;
    private InputAction moveAction = null;
    private InputAction jumpAction = null;
    private InputAction lookAction = null;
    private Quaternion rotation = Quaternion.identity;
    private float xRotation = 0.0f;
    private bool isJumping = false;
    private bool isFalling = false;

    private Vector3 spawnPos = Vector3.zero;
    private GameTimer gameTimer = null;
    private bool gameTimerStarted = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        input = new PlayerInput();
        moveAction = input.Player.Move;
        jumpAction = input.Player.Jump;
        lookAction = input.Player.Look;

        jumpAction.performed += OnJump;
        Cursor.lockState = CursorLockMode.Locked;

        spawnPos = transform.position;

        gameTimer = Object.FindFirstObjectByType<GameTimer>();
    }

    private void OnEnable()
    {
        input.Enable();
        moveAction.Enable();
        jumpAction.Enable();
        lookAction.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
        moveAction.Disable();
        jumpAction.Disable();
        lookAction.Disable();
    }

    private void Update()
    {

        // moveInput.x = a/d or left/right
        // moveInput.y = w/s or up/down
        // moveInput = left joystick / d-pad
        Vector2 moveInput = moveAction.ReadValue<Vector2>();


        Vector3 fwd = rigidBody.transform.forward;
        Vector3 right = rigidBody.transform.right;
        fwd.y = 0.0f;
        right.y = 0.0f;
        fwd.Normalize();
        right.Normalize();

        Vector3 moveVelocity = (fwd * moveInput.y * moveSpeed) + (right * moveInput.x * moveSpeed);
        moveVelocity.y = rigidBody.linearVelocity.y;

        rigidBody.linearVelocity = moveVelocity;
        rigidBody.angularVelocity = Vector3.zero;

        // Look action
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        Vector2 lookDelta = Vector2.zero;

        if (lookInput.sqrMagnitude > lookDeadZone * lookDeadZone)
        {
            lookDelta = lookInput * lookSensitivity * Time.deltaTime;
        }

        rotation = Quaternion.Euler(0.0f, lookDelta.x, 0.0f);
        rotation = rigidBody.rotation * rotation;

        if (invertY)
        {
            xRotation -= lookDelta.y;
        }
        else
        {
            xRotation += lookDelta.y;
        }
        xRotation = Mathf.Clamp(xRotation, minLookDown, maxLookUp);

        if (!gameTimerStarted && moveAction.ReadValue<Vector2>().sqrMagnitude > 0.1f)
        {
            gameTimer.StartTimer();
            gameTimerStarted = true;
        }

        if (transform.position.y < -10.0f)
        {
            Die();
        }

        if(!groundCheck.IsGrounded && rigidBody.linearVelocity.y < 0.1f)
        {
            isFalling = true;
            isJumping = false;
        }
        else if (groundCheck.IsGrounded)
        {
            isFalling = false;
        }

        float currentSpeed = moveVelocity.magnitude;

        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsFalling", isFalling);
        animator.SetFloat("State", currentSpeed / moveSpeed);
        animator.SetFloat("Vert", currentSpeed / moveSpeed);
    }

    private void LateUpdate()
    {
        lookTarget.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        rigidBody.MoveRotation(rotation);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (groundCheck.IsGrounded && rigidBody.linearVelocity.y < 0.1f)
        {
            Vector3 velocity = rigidBody.linearVelocity;
            velocity.y = jumpSpeed;
            rigidBody.linearVelocity = velocity;
            isJumping = true;
            isFalling = false;
        }
    }

    private void Die()
    {
        PlayerController player = GetComponent<PlayerController>();
        if (player != null)
        {
            player.transform.position = spawnPos;
            player.rigidBody.linearVelocity = Vector2.zero;
        }

    }
    public void UpdateCheckpoint(Vector3 newSpawnPos)
    {
        spawnPos = newSpawnPos + Vector3.left * checkPointOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            if (gameTimer != null)
            {
                gameTimer.StopTimer();
            }

            // You could also show a UI popup or restart the level here
            Debug.Log("Final Time: " + gameTimer.GetElapsedTime().ToString("F2"));
        }
    }
}

