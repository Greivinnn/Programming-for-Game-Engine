using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        EndGame();
    }

    private void EndGame()
    {
        Debug.Log("LEVEL COMPLETE!");
        Time.timeScale = 0f; // Freeze game
    }
}
