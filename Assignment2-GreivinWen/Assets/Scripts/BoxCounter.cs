using TMPro;
using UnityEngine;

public class BoxCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private int maxBoxes = 5; // set same as spawner
    private int boxesRemaining;

    public static BoxCounter instance;

    private void Awake()
    {
        instance = this;
    }

    public static void ResetToMax()
    {
        if (instance != null)
        {
            instance.boxesRemaining = instance.maxBoxes;
            instance.UpdateText();
        }
    }

    private void Start()
    {
        boxesRemaining = maxBoxes;
        UpdateText();

        SpawnBox.OnBoxSpawned += BoxUsed;
        SpawnBox.OnBoxDestroyed += BoxReturned;
    }

    private void OnDestroy()
    {
        SpawnBox.OnBoxSpawned -= BoxUsed;
        SpawnBox.OnBoxDestroyed -= BoxReturned;
    }

    private void BoxUsed()
    {
        boxesRemaining--;
        UpdateText();
    }

    private void BoxReturned()
    {
        boxesRemaining++;
        UpdateText();
    }

    private void UpdateText()
    {
        counterText.text = "Boxes Left: " + boxesRemaining;
    }
}
