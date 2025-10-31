using UnityEngine;
using UnityEngine.InputSystem;

public class DevTeleport : MonoBehaviour
{
    public Transform debugSpawn;

    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            transform.position = debugSpawn.position;
        }
    }
}
