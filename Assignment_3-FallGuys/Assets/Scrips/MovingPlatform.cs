using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float moveDuration = 2.0f;
    [SerializeField]
    private Transform endTransform = null;
    private Vector3 startingPosition = Vector3.zero;
    private bool goToEnd = true;
    private float moveTime = 0.0f;
    void Awake()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        moveTime += Time.deltaTime;

        if(goToEnd)
        {
            transform.position = Vector3.Lerp(startingPosition, endTransform.position, Mathf.Clamp(moveTime / moveDuration, 0.0f, 1.0f));
            if(moveTime>= moveDuration)
            {
                goToEnd = false;
                moveTime = 0.0f;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(endTransform.position, startingPosition, Mathf.Clamp(moveTime / moveDuration, 0.0f, 1.0f));
            if (moveTime >= moveDuration)
            {
                goToEnd = true;
                moveTime = 0.0f;
            }
        }
    }
}
