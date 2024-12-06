using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class PlayerCursor : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private World world;

        public Vector3Int Position {get; private set;}
        private void OnEnable()
        {
            input.actions.FindAction("Select").started += SelectField;
        }

        private void OnDisable()
        {
            input.actions.FindAction("Select").started -= SelectField;
        }

        private void SelectField(InputAction.CallbackContext obj)
        {
            var cell = world.Buildings.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Position = cell;
            MoveToCell(Position);
        }

        private void MoveToCell(Vector3Int pos)
        {
            print($"Moving to {pos}");
            transform.position = world.Buildings.CellToWorld(pos);
        }
    }
}