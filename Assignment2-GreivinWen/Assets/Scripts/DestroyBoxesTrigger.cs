using UnityEngine;

public class DestroyBoxesTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        DestroyAllBoxes();
        ResetSpawners();
        BoxCounter.ResetToMax();
    }

    private void DestroyAllBoxes()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");

        foreach (GameObject box in boxes)
        {
            Destroy(box);
        }
    }

    private void ResetSpawners()
    {
        SpawnBox[] spawners = FindObjectsOfType<SpawnBox>();

        foreach (SpawnBox spawner in spawners)
        {
            spawner.ResetSpawnCount();
        }
    }
}
