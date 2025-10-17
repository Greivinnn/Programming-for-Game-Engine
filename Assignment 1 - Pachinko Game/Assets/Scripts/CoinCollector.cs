using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField]
    private int coinValue = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("PachinkoBall"))
        {
            GameManager.Instance.AddToScore(coinValue);
            Debug.Log($"Coin collected! Added {coinValue} points");
            Destroy(gameObject); // Destroys the coin
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
