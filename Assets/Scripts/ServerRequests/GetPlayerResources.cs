using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class GetPlayerResources : IServerRequestUser
    {
        public string username;
        public event Action<PlayerData> RequestCompleted;

        public GetPlayerResources(string username) => this.username = username;

        public async Task Request(string gameuuid)
        {
            RequestCompleted?.Invoke(await ServerRequest.GetPlayer(gameuuid, username));
        }
    }
}