using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class GetPlayersRequest : IServerRequestUser
    {
        public event Action<PlayerData[]> RequestCompleted;

        public async Task Request(string gameuuid)
        {
            RequestCompleted?.Invoke(await ServerRequest.GetPlayers(gameuuid));
        }
    }
}
