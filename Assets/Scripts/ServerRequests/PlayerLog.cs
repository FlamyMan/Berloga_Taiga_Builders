using System.Collections.Generic;

namespace Assets.Scripts.ServerRequests
{
    public class PlayerLog
    {
        public string comment;
        public string player_name;
        public Dictionary<string, string> resources_changed;
    }
}
