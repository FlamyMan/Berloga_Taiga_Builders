using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class CreateShopRequest : IServerRequestUser
    {
        public string username;
        public ShopData data;
        public event Action<ShopData> RequestCompleted;

        public CreateShopRequest(string username, ShopData data)
        {
            this.username = username;
            this.data = data;
        }

        public async Task Request(string gameuuid)
        {
            RequestCompleted?.Invoke(await ServerRequest.CreateShop(gameuuid, username, data));
        }
    }
}
