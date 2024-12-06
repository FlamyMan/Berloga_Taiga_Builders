using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public static class ServerRequest
    {
        public async static Task<RegisterGameOutput> RegisterGame()
        {
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
            return await RequestBehavior.Post<RegisterGameOutput>(url, req, RequestBehavior.JsonContentType);
        }

        public async static Task<PlayerData> CreatePlayer(string gameuuid, PlayerData data)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/", gameuuid);
            return await RequestBehavior.Post<PlayerData>(url, data);
        }

        public async static Task<PlayerData[]> GetPlayers(string gameuuid)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/", gameuuid);
            return await RequestBehavior.Get<PlayerData[]>(url);
        }

        public async static Task<PlayerData> GetPlayer(string gameuuid, string username)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/", gameuuid, username);
            return await RequestBehavior.Get<PlayerData>(url);
        }

        public async static Task<GameResources> UpdatePlayerResources(string gameuuid, string username, GameResources resources)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/", gameuuid, username);
            return await RequestBehavior.Put<GameResources>(url, resources);
        }

        public static async Task DeletePlayer(string gameuuid, string username)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/", gameuuid, username);
            await RequestBehavior.DeleteRaw(url);
        }

        public async static Task<ServerLog> CreatePlayerLog(string gameuuid, ServerLog logs)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/logs/", gameuuid);
            return await RequestBehavior.Post<ServerLog>(url, logs);
        }

        public async static Task<ServerLog[]> GetPlayersLogs(string gameuuid, string username)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/logs/", gameuuid, username);
            return await RequestBehavior.Get<ServerLog[]>(url);
        }

        public async static Task<ShopData> CreateShop(string gameuuid, string username, ShopData data)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/", gameuuid, username);
            return await RequestBehavior.Post<ShopData>(url, data);
        }

        public async static Task<ShopData[]> GetPlayerShops(string gameuuid, string username)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/", gameuuid, username);
            return await RequestBehavior.Get<ShopData[]>(url);
        }

        public async static Task<ShopData> GetShop(string gameuuid, string username, string shopname)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/{2}/", gameuuid, username, shopname);
            return await RequestBehavior.Get<ShopData>(url);
        }

        public async static Task<GameResources> UpdateShopResources(string gameuuid, string username, string shopName, GameResources resources)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/{2}/", gameuuid, username, shopName);
            return await RequestBehavior.Put<GameResources>(url, resources);
        }

        public static async Task DeleteShop(string gameuuid, string username, string shopName)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/{2}/", gameuuid, username, shopName);
            await RequestBehavior.DeleteRaw(url);
        }

        public static async Task<ServerLog> CreateShopLog(string gameuuid, ServerLog log)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/logs/", gameuuid);
            return await RequestBehavior.Post<ServerLog>(url, log);
        }

        public static async Task<ServerLog[]> GetShopLogs(string gameuuid, string username, string shopname)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/{2}/logs/", gameuuid, username, shopname);
            return await RequestBehavior.Get<ServerLog[]>(url);
        }

        public static async Task<ServerLog[]> GetAllLogs(string gameuuid)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/logs/", gameuuid);
            return await RequestBehavior.Get<ServerLog[]>(url);
        }
    }
}
