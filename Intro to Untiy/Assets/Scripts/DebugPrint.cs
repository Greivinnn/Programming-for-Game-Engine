using Unity.VisualScripting;
using UnityEngine;

public class DebugPrint : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f; // [SerializeField} lets you adjust the number in the game engine
    private bool firstUpdate = false;
    private bool firstFixedUpdate = false;
    private float lifeTime = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected void Awake()
    {
        Debug.Log("Awake Called");
    }
    void Start()
    {
        Debug.Log("Start Called");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update Called");

        lifeTime -= Time.deltaTime;
        if (lifeTime < moveSpeed)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!firstUpdate)
        {
            Debug.Log("FirstUpdate Called");
            firstUpdate = true;
        }
    }

    // once a collistion occurs we get this function called
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEntered2D Called");
    }

    // access to time in three different ways:
    // Time.deltaTime;
    //Time.unscaledDeltaTime;
    //Time.timeScaled

    private void OnDestroy()
    {
        Debug.Log("OnDestroy Called");
    }
}
