using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] 
    private int maxSpots = 2;
    [SerializeField]
    private int currentSpots = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentSpots == maxSpots)
        {
            return;
        }
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.SetCheckpoint(transform.position);

            currentSpots++;
        }
    }
}