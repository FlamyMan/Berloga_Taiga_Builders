using Assets.Scripts.ServerRequests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        private string _username;
        public string Username => _username;
        private Dictionary<string, int> _resourcesCount = new();
        public IReadOnlyDictionary<string, int> ResourcesCount => _resourcesCount;
        public event Action OnResourcesChanged;

        private const string oldPlayerResourcesPath = @"\oldResources.json";

        public async void Awake()
        {
            if (PlayerPrefs.HasKey("username"))
            {
                _username = PlayerPrefs.GetString("username");
                await GetAllResources();
            }
            else// new player
            {
                _username = Guid.NewGuid().ToString();
                if (await ServerRequest.CreatePlayer(new PlayerData(_username, null)) != null)
                {
                    await ServerRequest.CreatePlayerLog(new PlayerLog()
                    { comment = "New player was registered.",
                        player_name = _username,
                        resources_changed = new Dictionary<string, string>()
                    });
                    PlayerPrefs.SetString("username", _username);
                    PlayerPrefs.Save();
                    Debug.Log($"Player Created username {_username}");
                }
            }
        }

        public async Task UpdateItems(GameResources resources, string LogComment = null)
        {
            await GetAllResources();
            var updatedResources = new Dictionary<string, string>();
            var TotalResources = new GameResources() 
            {
                resources = new()
            };

            try
            {
                foreach (var resource in resources.resources)
                {
                    if (resource.Value > 0) updatedResources[resource.Key] = $"+{AddItem(resource.Key, resource.Value)}";
                    else if (resource.Value < 0)updatedResources[resource.Key] = $"-{TakeItem(resource.Key, -resource.Value)}";
                    TotalResources.resources.Add(resource.Key, resource.Value);
                }
            }
            catch (ArgumentException e)
            {
                Debug.Log(e);
                await GetAllResources();
                return;
            }

            await ServerRequest.UpdatePlayerResources(_username, TotalResources);
            if (LogComment != null)
            {
                await ServerRequest.CreatePlayerLog(new PlayerLog()
                {
                    comment = LogComment,
                    player_name = _username,
                    resources_changed = updatedResources
                });
            }
            WriteOldPlayerResources(_resourcesCount);
            OnResourcesChanged?.Invoke();
        }

        private int AddItem(string name, int count)
        {
            if (_resourcesCount.ContainsKey(name))
            {
                _resourcesCount[name] += count;
            }
            else
            {
                _resourcesCount.Add(name, count);
            }
            return count;
        }

        public int TakeItem(string name, int count)
        {
            if (_resourcesCount.ContainsKey(name) && _resourcesCount[name] >= count)
            {
                _resourcesCount[name] -= count;
            }
            else
            {
                throw new ArgumentException("Player don't have enough resource ", name);
            }
            return count;
        }

        private async Task GetAllResources()
        {
            var data = await ServerRequest.GetPlayer(_username);
            if (data == null) // network error
            {
                _resourcesCount = CheckOldPlayerResources().resources;
                return;
            }
            _resourcesCount = data.resources;
            if (_resourcesCount == null) 
            {
                _resourcesCount = new Dictionary<string, int>();
                await ServerRequest.UpdatePlayerResources(_username, new GameResources(){resources = _resourcesCount });
            };
            WriteOldPlayerResources(_resourcesCount);
            OnResourcesChanged?.Invoke();
        }

        public GameResources CheckOldPlayerResources()
        {
            string json = File.ReadAllText(Application.persistentDataPath + oldPlayerResourcesPath);
            var ret = new GameResources
            {
                resources = JsonConvert.DeserializeObject<Dictionary<string, int>>(json)
            };
            return ret;
        }

        public void WriteOldPlayerResources(Dictionary<string, int> resources)
        {
            File.WriteAllText(Application.persistentDataPath + oldPlayerResourcesPath, JsonConvert.SerializeObject(resources));
        }
    }
}
