using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Camera _Camera;

    void FixedUpdate()
    {
        UpdateCursorLocation();
    }

    private void UpdateCursorLocation()
    {
        var _mousePos = Mouse.current.position.ReadValue();
        if (_Camera != null && _mousePos != null)
        {
            var _localMousePos = _Camera.ScreenToWorldPoint(_mousePos);
            _localMousePos.z = transform.position.z;
            transform.position = _localMousePos;
        }
    }
}
