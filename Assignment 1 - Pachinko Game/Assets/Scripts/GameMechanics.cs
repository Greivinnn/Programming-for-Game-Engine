using System.Runtime.CompilerServices;
using System.Security.Claims;
using UnityEngine;
using System;

public class GameMechanics : MonoBehaviour
{
    // what i need:
    // make balls spawn when SPACE is presssed
    // be able to move the ball spawner with W,A,S,D

   
    [SerializeField]
    private float maxDistanceFromStart = 10.0f;  
    [SerializeField]
    private Vector3 startPosition = Vector3.zero;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private GameObject ballPrefab = null;


    private void Awake()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // move left with A
        {
            position.x -= moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // move right with D
        {
            position.x += moveSpeed * Time.deltaTime;
        }

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        GameObject bullet = Instantiate(ballPrefab, transform.position, Quaternion.identity);    // Instantiate creates a copy of the GameObject at runtime in the given position 
        //Quaternion.identity just tells the program "no rotation"
    }
}
