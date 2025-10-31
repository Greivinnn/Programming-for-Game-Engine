using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        endPos = startPos - new Vector3(0, moveDistance, 0);
        StartCoroutine(SpikeRoutine());
    }

    // IEnumerator is a return type use to create coroutines
    // coroutine lets you have a function with pause execution and return to it later logic
    private IEnumerator SpikeRoutine()
    {
        while(true)
        {
            // Move down fast
            yield return MoveSpike(startPos, endPos, downDuration);

            yield return new WaitForSeconds(stayDuration);

            // Move up slow
            yield return MoveSpike(endPos, startPos, upDuration);

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

        if(player != null)
        {
            player.Die();
        }
    }
}
