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
        //if (collision.gameObject.tag == "coin")
        //{
        //    GameManager.Instance.AddToScore(100);
        //    Destroy(collision.gameObject);
        //}
        if (collision.name.Contains("PachinkoBall"))
        {
            Award(10, collision);
            Destroy(gameObject);
        }
    }

    private void Award(int basePoints, Collider2D collision)
    {
        int finalPpoints = Mathf.RoundToInt(basePoints * scoreMultiplier);
        GameManager.Instance.AddToScore(finalPpoints);
    }
}
