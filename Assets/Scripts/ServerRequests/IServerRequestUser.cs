using System;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public interface IServerRequestUser
    {
        public Task Request(string gameuuid);
    }
}
