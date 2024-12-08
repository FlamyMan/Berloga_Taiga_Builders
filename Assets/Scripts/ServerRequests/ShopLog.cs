using System.Collections.Generic;

namespace Assets.Scripts.ServerRequests
{
    public class ShopLog
    {
        public string comment;
        public string player_name;
        public string shop_name;
        public Dictionary<string, string> resources_changed;
    }
}
