using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class GetPlayerShopsRequest : IServerRequestUser
    {
        public string username;
        public event Action<ShopData[]> RequestCompleted;

        public GetPlayerShopsRequest(string username)
        {
            this.username = username;
        }

        public async Task Request(string gameuuid)
        {
            RequestCompleted?.Invoke(await ServerRequest.GetPlayerShops(gameuuid, username));
        }
    }
}
