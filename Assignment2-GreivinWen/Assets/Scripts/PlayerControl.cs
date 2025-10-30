using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float jumpSpeed = 15.0f;
    [SerializeField]
    //private GroundCheck groundCheck = null;

    private Animator animator = null;

    private Rigidbody2D rigidBody = null;
    private float direction = 1.0f;
    private bool isJumping = false;
    private bool isFalling = false; 
    private Vector3 startingPosition = Vector3.zero;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        if(Input.GetKey(KeyCode.A) && gameObject.name.Contains("FrogNinja")) // move left with A
        {
            position.x -= speed * Time.deltaTime;
            direction = -1.0f;
        }
        else if(Input.GetKey(KeyCode.D) && gameObject.name.Contains("FrogNinja")) // move right with D
        {
            position.x += speed * Time.deltaTime;
            direction = 1.0f;
        }
    }
}
