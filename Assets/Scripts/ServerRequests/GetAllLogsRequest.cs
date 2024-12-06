using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class GetAllLogsRequest : IServerRequestUser
    {
        public event Action<ServerLog[]> RequestCompleted;

        public async Task Request(string gameuuid)
        {
            RequestCompleted?.Invoke(await ServerRequest.GetAllLogs(gameuuid));
        }
    }
}
