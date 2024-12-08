using System.Threading.Tasks;

namespace Assets.Scripts.ServerRequests
{
    public static class ServerRequest
    {
        public const string gameuuid = "5990a4df-df07-4d44-98b4-c6aa295b87e7";
        public async static Task<PlayerData> CreatePlayer(PlayerData data)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/", gameuuid);
            return await RequestBehavior.Post<PlayerData>(url, data);
        }

        public async static Task<PlayerData[]> GetPlayers()
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/", gameuuid);
            return await RequestBehavior.Get<PlayerData[]>(url);
        }

        public async static Task<PlayerData> GetPlayer(string username)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/", gameuuid, username);
            return await RequestBehavior.Get<PlayerData>(url);
        }

        public async static Task<GameResources> UpdatePlayerResources(string username, GameResources resources)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/", gameuuid, username);
            return await RequestBehavior.Put<GameResources>(url, resources);
        }

        public static async Task DeletePlayer(string username)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/", gameuuid, username);
            await RequestBehavior.DeleteRaw(url);
        }

        public async static Task<ShopLog> CreatePlayerLog(PlayerLog logs)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/logs/", gameuuid);
            return await RequestBehavior.Post<ShopLog>(url, logs);
        }

        public async static Task<ShopLog[]> GetPlayersLogs(string username)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/logs/", gameuuid, username);
            return await RequestBehavior.Get<ShopLog[]>(url);
        }

        public async static Task<ShopData> CreateShop(string username, ShopData data)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/", gameuuid, username);
            return await RequestBehavior.Post<ShopData>(url, data);
        }

        public async static Task<ShopData[]> GetPlayerShops(string username)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/", gameuuid, username);
            return await RequestBehavior.Get<ShopData[]>(url);
        }

        public async static Task<ShopData> GetShop(string username, string shopname)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/{2}/", gameuuid, username, shopname);
            return await RequestBehavior.Get<ShopData>(url);
        }

        public async static Task<GameResources> UpdateShopResources(string username, string shopName, GameResources resources)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/{2}/", gameuuid, username, shopName);
            return await RequestBehavior.Put<GameResources>(url, resources);
        }

        public static async Task DeleteShop(string username, string shopName)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/{2}/", gameuuid, username, shopName);
            await RequestBehavior.DeleteRaw(url);
        }

        public static async Task<ShopLog> CreateShopLog(ShopLog log)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/logs/", gameuuid);
            return await RequestBehavior.Post<ShopLog>(url, log);
        }

        public static async Task<ShopLog[]> GetShopLogs(string username, string shopname)
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/players/{1}/shops/{2}/logs/", gameuuid, username, shopname);
            return await RequestBehavior.Get<ShopLog[]>(url);
        }

        public static async Task<ShopLog[]> GetAllLogs()
        {
            string url = string.Format("https://2025.nti-gamedev.ru/api/games/{0}/logs/", gameuuid);
            return await RequestBehavior.Get<ShopLog[]>(url);
        }
    }
}
