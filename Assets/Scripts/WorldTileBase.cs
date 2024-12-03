using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "World Tile Base", menuName = "World Objects")]
    public class WorldTileBase : ScriptableObject
    {
        public int id;
        public Vector3 size;
        public Sprite Sprite;
    }
}
