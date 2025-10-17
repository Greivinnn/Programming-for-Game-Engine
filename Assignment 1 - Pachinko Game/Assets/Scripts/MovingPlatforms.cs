using UnityEngine;
using System;
using UnityEngine.InputSystem.Controls;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float moveDistance = 3.0f;
    private float moveDirection = 1.0f;
    private Vector3 startPosition = Vector3.zero;
 

    void Awake()
    {
        startPosition = transform.position;
    }

    private void Start()
    {
        if (gameObject.tag == "moveRight")
        {
            moveDirection = 1.0f;   // start right
        }
        else if (gameObject.tag == "moveLeft")
        {
            moveDirection = -1.0f;  // start left
        }

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 position  = transform.position;
        position.x += moveSpeed * moveDirection * Time.deltaTime;

        if (gameObject.tag == "moveRight")
        {
            if (moveDirection > 0.0f && position.x > startPosition.x + moveDistance)
            {
                moveDirection = -1.0f;
            }
            else if (moveDirection < 0.0f && position.x < startPosition.x - moveDistance)
            {
                moveDirection = 1.0f;
            }
        }
        else if (gameObject.tag == "moveLeft")
        {
            if (moveDirection < 0.0f && position.x < startPosition.x - moveDistance)
            {
                moveDirection = 1.0f;
            }
            else if (moveDirection > 0.0f && position.x > startPosition.x + moveDistance)
            {
                moveDirection = -1.0f;
            }
        }
     
        transform.position = position;
    }
}
