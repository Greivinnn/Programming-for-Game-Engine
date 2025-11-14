using UnityEngine;
using UnityEngine.InputSystem;

public class AIController : MonoBehaviour
{
    private MouseClick inputActions = null;
    private InputAction clickAction = null;

    private void Awake()
    {
        inputActions = new MouseClick();
        clickAction = inputActions.Mouse.Click;
        clickAction.performed += OnMouseClick;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        clickAction.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        clickAction.Disable();
    }

    void OnMouseClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray cameraRay = Camera.main.ScreenPointToRay(mousePosition);
        if(Physics.Raycast(cameraRay, out RaycastHit hitInfo, 100.0f))
        {
            if(hitInfo.collider != null)
            {
                transform.position = hitInfo.point;
            }
        }
    }
}
