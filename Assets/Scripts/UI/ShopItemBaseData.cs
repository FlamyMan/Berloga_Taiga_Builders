using UnityEngine;

[CreateAssetMenu(fileName = "ItemGUIBase")]
public class ShopItemBaseData : ScriptableObject
{
    public string resource_id;
    public Texture2D Icon;
    public string ItemName;
    [TextArea] public string Description;
}

