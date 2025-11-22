using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private Rigidbody rigidBody = null;
    [SerializeField]
    private Transform lookTarget = null;
    [SerializeField]
    private GroundCheck groundCheck = null;
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float jumpSpeed = 10.0f;
    [SerializeField]
    private float mouseSensitivity = 100.0f;
    [SerializeField]
    private float mouseDeadZonce = 0.1f;
    [SerializeField]
    private bool invertY = false;

    private PlayerInput input = null;
    private InputAction moveAction = null;
    private InputAction lookAction = null;
    private InputAction jumpAction = null;

    private Vector3 moveVelocity = Vector3.zero;
    private Vector2 lookVelocity = Vector2.zero;
    private float xRotation = 0.0f; 

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        input = new PlayerInput();
        moveAction = input.Player.Move;
        lookAction = input.Player.Look;
        jumpAction = input.Player.Jump;

        jumpAction.performed += OnJump;

        
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if(IsOwner)
        {
            Cursor.lockState = CursorLockMode.Locked;

            CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
            CinemachineCamera camera = brain.ActiveVirtualCamera as CinemachineCamera;
            if (camera != null)
            {
                camera.Follow = lookTarget;
            }
        }
    }

    private void OnEnable()
    {
        input.Enable();
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (groundCheck.IsGrounded && rigidBody.linearVelocity.y < 0.1f)
        {
            Vector3 velocity = rigidBody.linearVelocity;
            velocity.y = jumpSpeed;
            rigidBody.linearVelocity = velocity;
        }
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        moveVelocity = transform.forward * moveInput.y * moveSpeed + transform.right * moveInput.x * moveSpeed;
        moveVelocity.y = rigidBody.linearVelocity.y;

        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        if (lookInput.magnitude < mouseDeadZonce)
        {
            lookInput = Vector2.zero;
        }
        lookVelocity = lookInput * mouseSensitivity * Time.deltaTime;

        rigidBody.linearVelocity = moveVelocity;
        rigidBody.angularVelocity = Vector3.zero;
        Quaternion rotation = rigidBody.rotation * Quaternion.Euler(0.0f, lookVelocity.x, 0.0f);
        rigidBody.MoveRotation(rotation);

        if(invertY)
        {
            xRotation -= lookVelocity.y;
        }
        else
        {
            xRotation += lookVelocity.y;
        }
        xRotation = Mathf.Clamp(xRotation, -80.0f, 80.0f);
        lookTarget.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
    }
}