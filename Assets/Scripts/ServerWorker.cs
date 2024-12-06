using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.ServerRequests;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

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
                _gameuuid = "5990a4df-df07-4d44-98b4-c6aa295b87e7";
                Debug.Log($"Game Registered UUID {_gameuuid}");
            }
            while (_running && requests.Count > 1)
            {
                if (_usesInternet) await requests.Dequeue().Request(_gameuuid);
            }
        }
    }
}
