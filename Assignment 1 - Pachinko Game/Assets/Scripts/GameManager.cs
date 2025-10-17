using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    // GameObject prefab from unity
    [SerializeField]
    private GameObject coinPrefab = null;
    [SerializeField]
    private GameObject bombPrefab = null;
    // how long is going to take to start spawning
    [SerializeField]
    private float spawnDelay = 5.0f;
    // how frequent is going to spawn once it starts 
    [SerializeField]
    private float spawnRate = 5.0f;
    // how many items to spawn each spawn time
    [SerializeField]
    private RangeInt spawnAmount = new RangeInt(1, 4);
    // spawn position
    [SerializeField]
    private Vector3 spawnPosition = Vector3.zero;
    // bomb chance
    private float bombChance = 0.2f;
    // spawn offset
    [SerializeField]
    private Vector3 halfSpawnOffset = Vector3.zero;

    private float nextSpawnTime = 0.0f;

    private int score = 0;

    // singleton in c# is:
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
    // all this. end of singleton in c#


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextSpawnTime = spawnDelay;
        UpdateScoreDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        nextSpawnTime -= Time.deltaTime;
        if (nextSpawnTime <= 0.0f)
        {
            SpawnItems();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        UpdateScoreDisplay();
    }

    private void SpawnItems()
    {
        int numSpawns = Random.Range(spawnAmount.start, spawnAmount.end + 1);
        for(int i = 0; i < numSpawns; ++i)
        {
            Vector3 spawnPos = spawnPosition;
            spawnPos.x += Random.Range(-halfSpawnOffset.x, halfSpawnOffset.x);
            spawnPos.y += Random.Range(-halfSpawnOffset.y, halfSpawnOffset.y);
            if(Random.Range(0.0f, 1.0f) < bombChance)
            {
                Instantiate(bombPrefab, spawnPos, Quaternion.identity);
            }
            else
            {
                Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            }
        }
        nextSpawnTime = spawnRate + Random.Range(-0.25f, 0.25f);
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
