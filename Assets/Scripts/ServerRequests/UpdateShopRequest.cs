using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class UpdateShopRequest : IServerRequestUser
    {
        public string username;
        public string shopname;
        public GameResources resources;
        public event Action<GameResources> RequestCompleted;

        public UpdateShopRequest(string username, string shopname, GameResources resources)
        {
            this.username = username;
            this.shopname = shopname;
            this.resources = resources;
        }

        public async Task Request(string gameuuid)
        {
            RequestCompleted?.Invoke(await ServerRequest.UpdateShopResources(gameuuid, username, shopname, resources));
        }
    }
}
