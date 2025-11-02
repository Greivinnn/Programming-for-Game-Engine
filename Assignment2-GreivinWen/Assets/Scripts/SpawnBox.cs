using UnityEngine;
using System.Collections;

public class SpawnBox : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject = null;
    [SerializeField]
    private int maxSpawnNumber = 1;
    [SerializeField]
    private Vector2 spawnOffset = new Vector2(1.0f, 0.0f);
    [SerializeField]
    private float spawnCooldown = 2f; // time between allowed spawns

    private int currentSpawnNumber = 0;
    private bool onCooldown = false;

    public static System.Action OnBoxSpawned;
    public static System.Action OnBoxDestroyed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentSpawnNumber >= maxSpawnNumber) return;
        if (onCooldown) return;
        if (!collision.CompareTag("Player")) return;

        SpawnBoxInstant();
        StartCoroutine(CooldownRoutine());
    }

    private void SpawnBoxInstant()
    {
        Vector3 spawnPos = transform.position + (Vector3)spawnOffset;
        Instantiate(spawnObject, spawnPos, Quaternion.identity);
        currentSpawnNumber++;
        OnBoxSpawned?.Invoke(); // notify counter
    }

    private IEnumerator CooldownRoutine()
    {
        onCooldown = true;
        yield return new WaitForSeconds(spawnCooldown);
        onCooldown = false;
    }

    public void ResetSpawnCount()
    {
        currentSpawnNumber = 0;
    }
}
