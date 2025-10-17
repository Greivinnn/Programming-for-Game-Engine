using UnityEngine;

public class SpinningPlatform : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeedClockwise = 900.0f;
    [SerializeField]
    private float rotationSpeedCounter = 700.0f;
    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "clockwise")
        {
            transform.Rotate(0.0f, 0.0f, rotationSpeedClockwise * Time.deltaTime);
        }
        if (gameObject.tag == "counterClockwise")
        {
            transform.Rotate(0.0f, 0.0f, -rotationSpeedCounter * Time.deltaTime);
        }
        
    }
}
