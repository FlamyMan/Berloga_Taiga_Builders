using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class CreateShopLogRequest : IServerRequestUser
    {
        public ServerLog log;
        public event Action<ServerLog> RequestCompleted;

        public CreateShopLogRequest(string comment, string username, string shopname, Dictionary<string, string> resources)
        {
            log = new ServerLog()
            {
                comment = comment,
                player_name = username,
                shop_name = shopname,
                resources_changed = resources
            };
        }

        public async Task Request(string gameuuid)
        {
            RequestCompleted?.Invoke(await ServerRequest.CreateShopLog(gameuuid, log));
        }
    }
}
