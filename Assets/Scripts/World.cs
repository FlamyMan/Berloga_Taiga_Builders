using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public class World : MonoBehaviour
    {
        public Tilemap BaseTilemap;
        public Tilemap Buildings;

        public Dictionary<Vector3Int, IBuilding> BuildingObjects;
        public Player Player;
    }
}
