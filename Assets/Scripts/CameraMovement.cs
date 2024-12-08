using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float cameraSpeed = 1;
    [SerializeField] private float maxX = 100;
    [SerializeField] private float maxY = 100;
    private InputAction _action;
    private void Awake()
    {
        _action = _input.actions.FindAction("Move");
    }

    private void Update()
    {
        transform.Translate(_action.ReadValue<Vector2>() * Time.deltaTime * cameraSpeed);
        if (!(-maxX <= transform.position.x && transform.position.x <= maxX) || !(-maxY <= transform.position.y && transform.position.y <= maxY))
        {
            transform.position = new Vector3(0, 0, transform.position.z);
        }
    }
}
