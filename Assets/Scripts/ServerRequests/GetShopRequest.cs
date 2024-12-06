using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class GetShopRequest : IServerRequestUser
    {
        public string username;
        public string shopname;
        public event Action<ShopData> RequestCompleted;

        public GetShopRequest(string username, string shopname)
        {
            this.username = username;
            this.shopname = shopname;
        }

        public async Task Request(string gameuuid)
        {
            RequestCompleted?.Invoke(await ServerRequest.GetShop(gameuuid, username, shopname));
        }
    }
}
