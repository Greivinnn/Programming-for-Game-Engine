using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timerText;
    private float startTime;
    private bool timerRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText.text = "Time: 00:00";
    }

    // Update is called once per frame
    void Update()
    {
        if(timerRunning)
        {
            float elapsed = Time.time - startTime;
            int minutes = Mathf.FloorToInt(elapsed / 60f);
            int seconds = Mathf.FloorToInt(elapsed % 60f);

            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public float GetElapsedTime()
    {
        return Time.time - startTime;
    }
}
