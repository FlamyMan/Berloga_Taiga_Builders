using Assets.Scripts.Buildings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public class World : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float tick_seconds = 1f;
        public Tilemap Buildings;

        public Dictionary<Vector3Int, Building> BuildingObjects;
        public Player Player;
        public event Action OnTick;
        private Dictionary<string, int> _tickResources = new Dictionary<string, int>();

        private async void PushResources()
        {
            if (_tickResources.Count == 0) return;
            var res = new GameResources
            {
                resources = _tickResources
            };
            await Player.UpdateItems(res, "Structures added resources to player");
            _tickResources.Clear();
        }

        public void AddTickResources(string name, int count)
        {
            if (_tickResources.ContainsKey(name))
            {
                _tickResources[name] += count;
            }
            else
            {
                _tickResources.Add(name, count);
            }
        }
        private void OnEnable()
        {
            StartCoroutine(Tick());
        }
        
        private IEnumerator Tick()
        {
            while (enabled)
            {
                OnTick?.Invoke();
                PushResources();
                yield return new WaitForSecondsRealtime(tick_seconds);
            }
        }
    }
}
