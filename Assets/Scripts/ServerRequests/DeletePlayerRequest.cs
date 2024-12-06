using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public class DeletePlayerRequest : IServerRequestUser
    {
        public string username;
        public event System.Action<object> RequestCompleted;

        public DeletePlayerRequest(string username) => this.username = username;

        public async Task Request(string gameuuid)
        {
            await ServerRequest.DeletePlayer(gameuuid, username);
            RequestCompleted?.Invoke(null);
        }
    }
}
