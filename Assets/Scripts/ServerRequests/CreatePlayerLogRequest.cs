using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    internal class CreatePlayerLogRequest : IServerRequestUser
    {
        public ServerLog serverLog;
        public event Action<ServerLog> RequestCompleted;

        public CreatePlayerLogRequest(string comment, string player_name, Dictionary<string, string> parameters)
        {
            serverLog = new();
            serverLog.comment = comment;
            serverLog.player_name = player_name;
            serverLog.resources_changed = parameters;
        }
        public async Task Request(string gameuuid)
        {
            RequestCompleted?.Invoke(await ServerRequest.CreatePlayerLog(gameuuid, serverLog));
        }
    }
}
