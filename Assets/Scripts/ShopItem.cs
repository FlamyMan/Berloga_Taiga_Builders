using Assets.Scripts.ServerRequests;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public class ShopItem
    {
        public Shop shop;
        public Player player;
        public PlayerCursor cursor;
        public ShopItemGUIData data;

        public event Action<TileBase> Chosen;

        public ShopItem(Shop shop, Player player, ShopItemGUIData data, PlayerCursor cursor) 
        {
            this.shop = shop;
            this.player = player;
            this.data = data;
            this.cursor = cursor;
        }

        public async void TryPlaceBuilding()
        {
            Debug.Log($"{shop}, {player}, {data}, {cursor}");
            if (player.ResourcesCount.ContainsKey(GameResources.ResourceHoney))
            {
                if (player.ResourcesCount[GameResources.ResourceHoney] >= data.Price)
                {
                    GameResources re = new()
                    {
                        resources = new()
                    };
                    re.resources.Add(GameResources.ResourceHoney, -data.Price);
                    await player.UpdateItems(re, $"Player bought {data.Base.ItemName}");
                    Chosen?.Invoke(data.Base.tileBase);
                }
            }
        }
    }
}
