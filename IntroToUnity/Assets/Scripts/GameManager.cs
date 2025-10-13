using UnityEngine;
using TMPro;

    public class GameManager : MonoBehaviour
    {
        //Manage spawns
        //Display points
        //Track win/lose 

        // Text object for displaying score
        [SerializeField]
        private TMP_Text scoreText = null;
        // Prefab for a mushroom
        [SerializeField]
        private GameObject mushroomsPrefab = null;
        // Prefab for a bomb
        [SerializeField]
        private GameObject bombPrefab = null;
        // How often to spawn
        [SerializeField]
        private float spawnDelay = 2.0f;
        // How frequently items spawn
        [SerializeField]
        private float spawnRate = 1.0f;
        // How many items to spawn each time
        [SerializeField]
        private RangeInt spawnAmount = new RangeInt(1, 3);
        // Chance of a bomb
        [SerializeField]
        private float bombChance = 0.1f;
        // Spawn position
        [SerializeField]
        private Vector3 spawnPosition = Vector3.zero;
        // Offset for spawn position
        [SerializeField]
        private Vector3 halfSpawnOffSet = Vector3.zero;

        private float nextSpawnTime = 0.0f;

        private int score = 0;

        private static GameManager instance = null;
        public static GameManager Instance { get { return instance; } }

        private void Awake()
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
            nextSpawnTime = spawnDelay;
        }

        // Update is called once per frame
        void Update()
        {
            nextSpawnTime -= Time.deltaTime;
            if (nextSpawnTime <= 0.0f)
            {
                SpawnItems();
            }
            UpdateScoreDisplay();
        }

        private void SpawnItems()
        {
            int numSpawns = Random.Range(spawnAmount.start, spawnAmount.end + 1);
            for (int i = 0; i < numSpawns; i++)
            {
                Vector3 sapwnPos = spawnPosition;
                sapwnPos.x += Random.Range(-halfSpawnOffSet.x, halfSpawnOffSet.x);
                sapwnPos.y += Random.Range(-halfSpawnOffSet.y, halfSpawnOffSet.y);
                if (Random.Range(0.0f, 1.0f) < bombChance)
                {
                    Instantiate(bombPrefab, sapwnPos, Quaternion.identity);
                }
                else
                {
                    Instantiate(mushroomsPrefab, sapwnPos, Quaternion.identity);
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
            scoreText.text = "Score: " + score.ToString();
        }
    }
