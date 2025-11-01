using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 targetPos;
    private bool moving = false;

    private void Awake()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(0, moveDistance, 0);
        targetPos = startPos;
    }

    private void Update()
    {
        if (!moving) return;

        // Move smoothly toward the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        // Stop when close enough
        if (Vector3.Distance(transform.position, targetPos) <= 0.001f)
        {
            transform.position = targetPos;
            moving = false;
        }
    }

    public void OpenDoor()
    {
        Debug.Log("Player entered the trigger area, door opened.");
        targetPos = endPos;
        moving = true;
    }

    public void CloseDoor()
    {
        targetPos = startPos;
        moving = true;
    }
}
