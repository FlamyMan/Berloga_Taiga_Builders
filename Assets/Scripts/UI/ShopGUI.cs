using Assets.Scripts;
using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopGUI : MonoBehaviour
{
    [SerializeField] private PlayerCursor cursor;
    [SerializeField] private Player pl;

    [SerializeField] private UIDocument main;
    public ShopGUIData data;
    public VisualTreeAsset itemTemplate;

    private UIDocument document;
    private ScrollView scroll;
    private Button _button;

    private readonly Shop shop = new();

    public void Awake()
    {
        document = GetComponent<UIDocument>();
        shop.LoadShop("Building Shop", pl.name);
    }


    private void OnEnable()
    {
        scroll = document.rootVisualElement.Q<ScrollView>("items-scroll");
        document.rootVisualElement.dataSource = data;
        _button = document.rootVisualElement.Q<Button>("QuitButton");
        for (int i = 0; i < data.Items.Length; i++)
        {
            VisualElement itemVisual = itemTemplate.Instantiate();
            scroll.Add(itemVisual);
            scroll[i].dataSourcePath = PropertyPath.FromIndex(i);
            scroll[i].Q<VisualElement>("Icon-Image").style.backgroundImage = new StyleBackground(data.Items[i].Base.Icon);
        }
        _button.clicked += Close;
    }

    private void OnDisable()
    {
        if(_button != null) _button.clicked -= Close;
    }

    private void Close()
    {
        main.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
[Serializable]
public class ShopGUIData
{
    public ShopItemGUIData[] Items;
}

[Serializable]
public struct ShopItemGUIData
{
    public ShopItemBaseData Base;
    public int Price;
}

