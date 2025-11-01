using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private float moveDistance = 2.0f;
    [SerializeField]
    private float upDuration = 1.0f;
    [SerializeField]
    private float downDuration = 0.3f;
    [SerializeField]
    private float stayDuration = 1.0f;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool isDoorOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(0, moveDistance, 0);
        isDoorOpen = false;
    }

    public void StartCoroutineFunc()
    {
        OpenDoor();
    }

    // Public methods to open/close the door
    public void OpenDoor()
    {
        if (isDoorOpen) return;
        isDoorOpen = true;
        StopAllCoroutines();
        StartCoroutine(DoorMovement());
    }

    public void CloseDoor()
    {
        if (!isDoorOpen) return;
        isDoorOpen = false;
        StopAllCoroutines();
        StartCoroutine(DoorMovement());
    }

    // Coroutine that handles opening (slow) and closing (fast)
    private IEnumerator DoorMovement()
    {
        if (isDoorOpen)
        {
            // Move from start to end (up) using upDuration
            yield return MoveDoor(startPos, endPos, upDuration);
            yield return new WaitForSeconds(stayDuration);
        }
        else
        {
            // Move from end to start (down) using downDuration (faster)
            yield return MoveDoor(endPos, startPos, downDuration);
        }
    }

    // Smoothly interpolates position from 'from' to 'to' over 'duration' seconds.
    private IEnumerator MoveDoor(Vector3 from, Vector3 to, float duration)
    {
        if (duration <= 0f)
        {
            transform.position = to;
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = to;
    }
}
