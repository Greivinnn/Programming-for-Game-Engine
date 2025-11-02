using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private bool oneTime = true;
    
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated && oneTime) return;

        var player = collision.GetComponent<PlayerController>();

        player.SetCheckPoint(transform.position);
        activated = true;
    }
}
