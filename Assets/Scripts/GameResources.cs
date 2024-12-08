using System.Collections.Generic;

namespace Assets.Scripts
{
    public class GameResources
    {
        public const string ResourceHoney = nameof(ResourceHoney);
        public const string ResourceWood = nameof(ResourceWood);
        public const string ResourceStone = nameof(ResourceStone);

        public Dictionary<string, int> resources;
    }
}