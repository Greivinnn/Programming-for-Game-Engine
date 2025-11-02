using UnityEngine;

public class SpawnBox : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject = null;
    [SerializeField]
    private int maxSpawnNumber = 1;
    [SerializeField]
    private Vector2 spawnOffset = new Vector2(1.0f, 0.0f);

    private int currentSpawnNumber = 0;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentSpawnNumber >= maxSpawnNumber) return;
        Vector3 spawnPos = transform.position + (Vector3)spawnOffset;
        Instantiate(spawnObject, spawnPos, Quaternion.identity);
        currentSpawnNumber++;
    }
}
