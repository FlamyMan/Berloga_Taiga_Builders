using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class GetPlayerLogsRequest : IServerRequestUser
    {
        public string username;
        public event Action<ServerLog[]> RequestCompleted;

        public GetPlayerLogsRequest(string username)
        {
            this.username = username;
        }

        public async Task Request(string gameuuid)
        {
            RequestCompleted?.Invoke(await ServerRequest.GetPlayersLogs(gameuuid, username));
        }
    }
}
