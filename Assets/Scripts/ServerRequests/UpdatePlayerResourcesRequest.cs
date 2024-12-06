using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class UpdatePlayerResourcesRequest : IServerRequestUser
    {
        public string username;
        public GameResources resources;
        public event Action<GameResources> RequestCompleted;

        public UpdatePlayerResourcesRequest(string username, GameResources resources)
        {
            this.username = username;
            this.resources = resources;
        }

        public async Task Request(string gameuuid)
        {
            RequestCompleted?.Invoke(await ServerRequest.UpdatePlayerResources(gameuuid, username, resources));
        }
    }
}
