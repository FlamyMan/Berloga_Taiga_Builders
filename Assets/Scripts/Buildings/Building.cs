using UnityEngine;


namespace Assets.Scripts.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        protected bool started = false;
        private Vector3Int _position;
        public Vector3Int Position
        {
            get { return _position; } 
            set { _position = value; }
        }

        private World _world;
        public World WorldParent => _world;

        protected void Start()
        {
            _world = gameObject.GetComponentInParent<World>();
            _position = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
            started = true;
        }

        public abstract void OnWorldTick();
    }
}
