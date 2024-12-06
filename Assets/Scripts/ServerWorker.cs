using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.ServerRequests;
using UnityEngine;

namespace Assets.Scripts
{
    public class ServerWorker : MonoBehaviour
    {
        private readonly Queue<IServerRequestUser> requests = new();

        private string _gameuuid;
        private bool _running;

        private bool _usesInternet => Application.internetReachability != NetworkReachability.NotReachable;
        private void OnEnable()
        {
            _running = true;
            DoRequests();
        }

        private void OnDisable()
        {
            _running = false;
        }

        public void AddRequest(IServerRequestUser request)
        {
            requests.Enqueue(request);
        }

        private async void DoRequests()
        {
            if (_usesInternet && _running)
            {
                _gameuuid = (await ServerRequest.RegisterGame()).uuid;
            }
            while (_running)
            {
                if (_usesInternet) await requests.Dequeue().Request(_gameuuid);
            }
        }
    }
}
