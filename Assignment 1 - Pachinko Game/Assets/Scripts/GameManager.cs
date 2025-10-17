using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    private int score = 0;

    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreDisplay();
    }

    public void AddToScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
    }
    private void UpdateScoreDisplay()
    {
        scoreText.text = "Current Score:" + score.ToString();
    }

    
}
