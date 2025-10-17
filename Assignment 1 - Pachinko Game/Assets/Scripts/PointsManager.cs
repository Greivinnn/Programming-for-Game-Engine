using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [SerializeField]
    private float scoreMultiplier = 1.0f;

    public void SetMultiplier(float newMultiplier)
    {
        scoreMultiplier = Mathf.Max(0.0f, newMultiplier);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("coin") && collision.name.Contains("PachinkoBall"))
        {
            GameManager.Instance.AddToScore(100);
            Debug.Log($"Collided with: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");
            Destroy(collision.gameObject);
        }
        else
        {
            Award(10, collision);
            Destroy(collision.gameObject);
        }
    }

    private void Award(int basePoints, Collider2D collision)
    {
        int finalPpoints = Mathf.RoundToInt(basePoints * scoreMultiplier);
        GameManager.Instance.AddToScore(finalPpoints);
    }
}
