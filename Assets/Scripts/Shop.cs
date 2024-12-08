using Assets.Scripts.ServerRequests;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;

namespace Assets.Scripts
{
    public class Shop
    {
        public string Name { get; private set; }
        public string Username {  get; private set; }
        private Dictionary<string, int> Prices = new();
        private string savePath;

        public async void LoadShop(string name, string username)
        {
            savePath = Application.persistentDataPath + @"\" + name + ".json";
            Name = name;
            Username = username;
            if (PlayerPrefs.HasKey(name))
            {
                await GetShopPrices();
            }
            else
            {
                var sd = new ShopData() { name = name};
                if (await ServerRequest.CreateShop(Username, sd) != null)
                {
                    await ServerRequest.CreateShopLog(new ShopLog() { comment = "New Shop Initialized", player_name = username, shop_name = name, resources_changed = new Dictionary<string, string>() });
                    PlayerPrefs.SetInt(name, 1);
                    PlayerPrefs.Save();
                }
            }
        }

        public async Task GetShopPrices()
        {
            ShopData d = await ServerRequest.GetShop(Username, Name);
            if (d != null)
            {
                Prices = d.resources;
            }
            else
            {
                var val = CheckOldShopPrices().resources;
                if (val != null) 
                {
                    Prices = val;
                }
                else
                {
                    Prices = new Dictionary<string, int>();
                }
            }
            WriteOldShopData(Prices);
        }

        public GameResources CheckOldShopPrices()
        {
            string json = File.ReadAllText(savePath);
            var ret = new GameResources
            {
                resources = JsonConvert.DeserializeObject<Dictionary<string, int>>(json)
            };
            return ret;
        }

        public void WriteOldShopData(Dictionary<string, int> resources)
        {
            File.WriteAllText(savePath, JsonConvert.SerializeObject(resources));
        }
    }
}
