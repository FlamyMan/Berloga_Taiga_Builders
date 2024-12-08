using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System;

namespace Assets.Scripts
{
    public class PlayerCursor : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private World world;
        [SerializeField] public TileBase tileToPlace;
        public Camera mainCamera;
        public event Action OnPlaced;


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
                    print(cellPosition);
                    world.Buildings.SetTile(cellPosition, tileToPlace);
                    OnPlaced?.Invoke();
                    gameObject.SetActive(false);
                }
            }
        }

        
    }
}