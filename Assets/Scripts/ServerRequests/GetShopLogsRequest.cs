using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class GetShopLogsRequest : IServerRequestUser
    {
        public string username;
        public string shopname;
        public event Action<ServerLog[]> RequestCompleted;

        public GetShopLogsRequest(string username, string shopname) 
        { 
            this.username = username;
            this.shopname = shopname;
        }
        public async Task Request(string gameuuid)
        {
            RequestCompleted?.Invoke(await ServerRequest.GetShopLogs(gameuuid, username, shopname));
        }
    }
}
