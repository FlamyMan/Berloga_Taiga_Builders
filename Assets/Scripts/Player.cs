using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        private Dictionary<string, int> _resourcesCount;
        public IReadOnlyDictionary<string, int> ResourcesCount => _resourcesCount;

        public void AddItem(string name, int count)
        {
            if (_resourcesCount.ContainsKey(name))
            {
                _resourcesCount[name] += count;
            }
            else
            {
                _resourcesCount.Add(name, count);
            }
        }

        public void TakeItem(string name, int count)
        {
            if (_resourcesCount.ContainsKey(name) && _resourcesCount[name] >= count)
            {
                _resourcesCount[name] -= count;
            }
            else
            {
                throw new ArgumentException("Player don't have enough resource ", name);
            }
        }
    }
}
