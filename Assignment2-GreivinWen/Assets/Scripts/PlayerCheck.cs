using UnityEngine;

public class PlayerCheck : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player == null) return;

        if(collision.gameObject.name.Contains("AstroBoy") && this.CompareTag("GreenMashroom"))
        {
            player.Die();
            return;
        }

        if (collision.gameObject.name.Contains("FrogNinja") && gameObject.CompareTag("BlueMashroom"))
        {
            player.Die();
            return;
        }
    }
}
