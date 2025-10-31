using UnityEngine;
using System.Collections;

public class SpikeTrapHorizontal : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 2f;          // How far left/right it moves
    public float leftDuration = 0.3f;        // Fast going left or right (your choice)
    public float rightDuration = 1.5f;       // Slow return
    public float stayDuration = 0.5f;        // Pause at ends

    private Vector3 startPos;
    private Vector3 endPos;

    private void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(moveDistance, 0, 0);

        StartCoroutine(SpikeRoutine());
    }

    private IEnumerator SpikeRoutine()
    {
        while (true)
        {
            // Move to the right fast
            yield return MoveSpike(startPos, endPos, leftDuration);
            yield return new WaitForSeconds(stayDuration);

            // Move back left slow
            yield return MoveSpike(endPos, startPos, rightDuration);
            yield return new WaitForSeconds(stayDuration);
        }
    }

    private IEnumerator MoveSpike(Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = to;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.Die();
        }
    }
}
