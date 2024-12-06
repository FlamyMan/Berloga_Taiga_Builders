using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class CreatePlayerRequest : IServerRequestUser
    {
        public PlayerData data;
        public event Action<PlayerData> RequestCompleted;

        public CreatePlayerRequest(PlayerData data) => this.data = data;
        public async Task Request(string gameuuid)
        {
            var player = await ServerRequest.CreatePlayer(gameuuid, data);
            RequestCompleted?.Invoke(player);
        }
    }
}
