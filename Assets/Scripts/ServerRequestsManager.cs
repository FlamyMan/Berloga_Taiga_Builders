using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Assets.Scripts
{
    public class ServerRequest
    {
        private const string jsonContent = "application/json";
        public DownloadHandler downloadHandler;
        public event Action<DownloadHandler> OnDone;
        private bool used = false;
        public bool Used => used;
        private bool CheckUsage()
        {
            if (used)
            {
                throw new Exception("ServerRequest can't be used twice!");
            }
            used = true;
            return true;
        }
        public IEnumerable RegisterGame()
        {
            CheckUsage();
            const string url = "https://2025.nti-gamedev.ru/api/games/";
            string talant_id = "549507";
            DateTime currentTime = DateTime.UtcNow;
            string nonce = ((DateTimeOffset)currentTime).ToUnixTimeSeconds().ToString();
            string signature;
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes($"{talant_id}{nonce}"));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("X2"));
                signature = sb.ToString();
            }
            string req = "{\"team_name\": PYRO Studio}";
            using (UnityWebRequest www = UnityWebRequest.Post(url, req, jsonContent))
            {
                www.SetRequestHeader("Nonce", nonce);
                www.SetRequestHeader("Talant-Id", talant_id);
                www.SetRequestHeader("Signature", signature);
                yield return www.SendWebRequest();

                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
                downloadHandler = www.downloadHandler;
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable CreatePlayer(string gameUUID, string player_name, Dictionary<string, int> resources=null)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/", gameUUID);

            string data = JsonConvert.SerializeObject(new PlayerData(player_name, resources), Formatting.Indented);
            using (UnityWebRequest www = UnityWebRequest.Post(url, data, jsonContent))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable GetPlayers(string gameUUID)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/", gameUUID);

            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable GetPlayerResources(string gameuuid, string username)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/", gameuuid, username);

            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable UpdatePlayerResources(string gameuuid, string username, GameResources resources)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/", gameuuid, username);
            var data = JsonConvert.SerializeObject(resources, Formatting.Indented);
            using (UnityWebRequest www = UnityWebRequest.Put(url, data))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable DeletePlayer(string gameuuid, string username)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/", gameuuid, username);
            using (UnityWebRequest www = UnityWebRequest.Delete(url))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }
        public IEnumerable CreatePlayerLog(string gameuuid, string username, string comment, Dictionary<string, string> resources)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/logs/", gameuuid);
            var logs = new PlayerLogs();
            logs.comment = comment;
            logs.player_name = username;
            logs.resources_changed = resources;
            string data = JsonConvert.SerializeObject(logs, Formatting.Indented);
            using (UnityWebRequest www = UnityWebRequest.Post(url, data, jsonContent))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable GetPlayerLogs(string gameuuid, string username)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/logs/", gameuuid, username);

            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable CreateShop(string gameuuid, string username, string shopName, Dictionary<string, int> resources)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/", gameuuid, username);
            Shop shop = new();
            shop.name = shopName;
            shop.resources = resources;
            string data = JsonConvert.SerializeObject(shop, Formatting.Indented);
            using (UnityWebRequest www = UnityWebRequest.Post(url, data, jsonContent))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable GetPlayerShops(string gameuuid, string username)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/", gameuuid, username);

            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable GetShopResources(string gameuuid, string username, string shopname)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/{2}/", gameuuid, username, shopname);

            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable UpdateShopResources(string gameuuid, string username, string shopName, GameResources resources)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/{2}/", gameuuid, username, shopName);
            var data = JsonConvert.SerializeObject(resources, Formatting.Indented);
            using (UnityWebRequest www = UnityWebRequest.Put(url, data))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable DeleteShop(string gameuuid, string username, string shopName)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/{2}/", gameuuid, username, shopName);
            using (UnityWebRequest www = UnityWebRequest.Delete(url))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable CreateShopLog(string gameuuid, string username, string comment, string shopName, Dictionary<string, string> resourcesChanged)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/logs/", gameuuid);
            var shoplog = new ShopLog();
            shoplog.comment = comment;
            shoplog.player_name = username;
            shoplog.shop_name = shopName;
            shoplog.resources_changed = resourcesChanged;
            var data = JsonConvert.SerializeObject(shoplog, Formatting.Indented);
            using (UnityWebRequest www = UnityWebRequest.Post(url, data, jsonContent))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable GetShopLogs(string gameuuid, string username, string shopName)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/{2}/logs/", gameuuid, username, shopName);

            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }

        public IEnumerable GetAllLogs(string gameuuid)
        {
            CheckUsage();
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/logs/", gameuuid);

            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                downloadHandler = www.downloadHandler;
                if(www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
            }
            OnDone?.Invoke(downloadHandler);
        }
    }

    public class ShopLog
    {
        public string comment;
        public string player_name;
        public string shop_name;
        public Dictionary<string, string> resources_changed;
    }

    public class Shop
    {
        public string name;
        public Dictionary<string, int> resources;
    }

    public class PlayerLogs
    {
        public string comment;
        public string player_name;
        public Dictionary<string, string> resources_changed;
    }
    
    public class GameResources
    {
        public Dictionary<string, int> resources;
        public GameResources(Dictionary<string, int> resources) => this.resources = resources;
    }
    public class PlayerData
    {
        public string username;
        public Dictionary<string, int> resources;

        public PlayerData(string username, Dictionary<string, int> resources)
        {
            this.username = username;
            this.resources = resources;
        }
    }
}