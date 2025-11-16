using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private int groundContacts = 0;
    public bool IsGrounded { get { return groundContacts > 0; } }

    private void OnTriggerEnter(Collider collision)
    {
        ++groundContacts;
    }
    private void OnTriggerExit(Collider collision)
    {
        --groundContacts;
    }
}
