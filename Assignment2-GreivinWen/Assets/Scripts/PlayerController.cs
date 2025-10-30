//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerController : MonoBehaviour
//{
//    [SerializeField]
//    private float moveSpeed = 10.0f;
//    [SerializeField]
//    private float jumpSpeed = 30.0f;
//    [SerializeField]
//    private GroundCheck groundCheck = null;

//    [SerializeField]
//    private Animator animator = null;

//    private Rigidbody2D rigidBody = null;
//    private PlayerInput playerInput = null;
//    private InputAction moveAction = null;
//    private InputAction jumpAction = null;
//    private float direction = 1.0f;
//    private bool isJumping = false;
//    private bool isFalling = false;
//    private Vector3 startingPosition = Vector3.zero;
//    private void Awake()
//    {
//        rigidBody = GetComponent<Rigidbody2D>();
//        animator = GetComponent<Animator>();
//        playerInput = new PlayerInput();
//        moveAction = playerInput.Player.Move;
//        jumpAction = playerInput.Player.Jump;

//        jumpAction.performed += OnJump;

//        startingPosition = transform.position;
//    }

//    private void OnEnable()
//    {
//        playerInput.Enable();
//        moveAction.Enable();
//        jumpAction.Enable();
//    }

//    private void OnDisable()
//    {
//        playerInput.Disable();
//        moveAction.Disable();
//        jumpAction.Disable();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Vector2 moveInput = moveAction.ReadValue<Vector2>();
//        rigidBody.linearVelocityX = moveInput.x * moveSpeed;
        
//        if(Mathf.Abs(moveInput.x) > 0.1f)
//        {
//            direction = (moveInput.x > 0.0f) ? 1.0f : -1.0f;
//        }
//        float speed = Mathf.Abs(rigidBody.linearVelocityX);

//        if(!groundCheck.IsGrounded && rigidBody.linearVelocityY <= 0.1f)
//        {
//            isFalling = true;
//            isJumping = false;
//        }

//        animator.SetFloat("Speed", speed);
//        animator.SetFloat("Direction", direction);
//        animator.SetBool("IsJumping", isJumping);
//        animator.SetBool("IsFalling", isFalling);

//        if (isFalling && transform.position.y < -20.0f)
//        {
//            transform.position = startingPosition;
//        }
//    }

//    private void OnJump(InputAction.CallbackContext context)
//    {
//        if(groundCheck.IsGrounded && rigidBody.linearVelocityY <= 0.1f)
//        {
//            rigidBody.linearVelocityY = jumpSpeed;
//            isJumping = true;
//            isFalling = false;
//        }
//    }
//}

