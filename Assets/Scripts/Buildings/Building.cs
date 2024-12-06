using UnityEngine;


namespace Assets.Scripts.Buildings
{
    public abstract class Building : MonoBehaviour, IBuilding
    {
        private Vector3Int _position;
        public Vector3Int Position
        {
            get { return _position; } 
            set { _position = value; }
        }

        private World _world;
        public World WorldParent => _world;

        public abstract void OnWorldTick();
    }
}
