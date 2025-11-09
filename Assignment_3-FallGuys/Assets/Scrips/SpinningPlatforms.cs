using UnityEngine;

public class SpinningPlatforms : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb = null;
    [SerializeField]
    private float spinSpeed = 100.0f;
    [SerializeField]
    private Vector3 spinAxis = Vector3.right;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.Rotate(spinAxis * spinSpeed * Time.deltaTime);
    }
}
