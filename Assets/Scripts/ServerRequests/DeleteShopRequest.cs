using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class DeleteShopRequest : IServerRequestUser
    {
        public string username;
        public string shopname;
        public event Action<object> RequestCompleted;

        public DeleteShopRequest(string username, string shopname)
        {
            this.username = username;
            this.shopname = shopname;
        }
        public async Task Request(string gameuuid)
        {
            await ServerRequest.DeleteShop(gameuuid, username, shopname);
            RequestCompleted?.Invoke(null);
        }
    }
}
