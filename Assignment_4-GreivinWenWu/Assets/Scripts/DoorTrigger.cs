using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject doorObject = null;
    [SerializeField]
    private float openAngle = 90.0f;
    [SerializeField]
    private float openSpeed = 2.0f;

    private bool isOpen = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        isOpen = true;

        Quaternion initialRotation = doorObject.transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, openAngle, 0);

        float elapsedTime = 0.0f;
        while (elapsedTime < 1.0f)
        {
            doorObject.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        doorObject.transform.rotation = targetRotation;
    }
}
