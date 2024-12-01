using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "World Tile Data", menuName ="World Objects")]
    public class WorldTileData : ScriptableObject
    {
        public int id;
        public Dictionary<string, int> IntValues = new();
        public Dictionary<string, float> FloatValues = new();
        public Dictionary<string, string> StringValues = new();
    }
}
