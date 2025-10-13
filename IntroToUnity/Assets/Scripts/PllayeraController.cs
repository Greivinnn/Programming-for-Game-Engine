using System;
using UnityEngine;

public class PllayeraCOntroller : MonoBehaviour
{
    // 1. we want to move the object left and right
    // - input system
    // - speed system
    // - update each frame in the Update void
    // 2. we want to prevent the object from moving too far left/right
    // 3. we want to catch falling objects
    // 4. we want to get points if objects is good, lose if objects are bad
    // 5. mushroon if it misses player (hit the ground)
    // 6. bomb
    // 7. UI/Points/Game Manager
    // - spawning drops
    
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float maxDistanceFromStart = 20.0f;
    private Vector3 startPosition = Vector3.zero; 
    
    private int gameCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        startPosition = transform.position;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // move left with A
        {
            position.x -= moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // move right with D
        {
            position.x += moveSpeed * Time.deltaTime;
        }
        //if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) // move down with S
        //{
        //    position.y -= moveSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) // move up with W
        //{
        //    position.y += moveSpeed * Time.deltaTime;
        //}

        float distanceFromStart = position.x - startPosition.x;
        if (Math.Abs(distanceFromStart) > maxDistanceFromStart)
        {
            if (distanceFromStart < 0)
            {
                position.x = startPosition.x - maxDistanceFromStart;
            }
            else
            {
                position.x = startPosition.x + maxDistanceFromStart;
            }
        }

        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject != null)
        {
            if (collision.gameObject.tag == "Mushroom")
            {
                Debug.Log("Mushroom gets points");
            }
            Destroy(collision.gameObject);
        }
    }
    
}
