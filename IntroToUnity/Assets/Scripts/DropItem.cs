using UnityEngine;

public class DropItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null && collision.gameObject != null)
        {
            if(collision.gameObject.tag != "Player")
            {
                Destroy(gameObject);
            }
        }
    }
}
