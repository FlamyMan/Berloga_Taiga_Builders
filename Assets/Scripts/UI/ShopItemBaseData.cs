using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ItemGUIBase")]
public class ShopItemBaseData : ScriptableObject
{
    public Texture2D Icon;
    public string ItemName;
    [TextArea] public string Description;
    public TileBase tileBase;
}

