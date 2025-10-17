using UnityEngine;

public class BombCollection : MonoBehaviour
{
    [SerializeField]
    private int bombValue = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("PachinkoBall"))
        {
            GameManager.Instance.AddToScore(-bombValue);
            Destroy(gameObject); // Destroys the coin
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
