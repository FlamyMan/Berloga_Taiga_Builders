using UnityEngine;

namespace Assets.Scripts
{
    public interface IBuilding
    {
        public Vector3Int Position { get; set; }
        public World WorldParent { get; }
        public void OnWorldTick();
    }
}
