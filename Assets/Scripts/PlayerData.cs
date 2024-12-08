using System.Collections.Generic;

namespace Assets.Scripts
{
    public class PlayerData
    {
        public string name;
        public Dictionary<string, int> resources = null;

        public PlayerData(string username, Dictionary<string, int> resources)
        {
            this.name = username;
        }
    }
}