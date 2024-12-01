using UnityEngine;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour
{
    private Tilemap _tilemap;

    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }


}
