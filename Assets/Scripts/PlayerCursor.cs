using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public class PlayerCursor : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private World world;

        public Camera mainCamera;      

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                Vector3 screenPosition = Input.mousePosition;

                Ray ray = mainCamera.ScreenPointToRay(screenPosition);

                Plane plane = new Plane(Vector3.forward, Vector3.zero);

                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 worldPosition = ray.GetPoint(distance);

                    Vector3Int cellPosition = world.Buildings.WorldToCell(worldPosition);

                    Vector3 tileCenter = world.Buildings.GetCellCenterWorld(cellPosition);

                    Debug.Log($"Clicked Cell: {cellPosition}, Tile Center: {tileCenter}");
                    transform.position = tileCenter;
                }
            }
        }
    }
}