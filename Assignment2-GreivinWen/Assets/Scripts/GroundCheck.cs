using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private int groundContacts = 0;
    public bool IsGrounded { get { return groundContacts > 0; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            ++groundContacts;
        }
        else
        {
            return;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            --groundContacts;
        }
        else
        {
            return;
        }
    }
}
