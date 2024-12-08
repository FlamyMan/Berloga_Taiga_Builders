using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class MainGUI : MonoBehaviour
    {
        private UIDocument _document;
        [SerializeField] private Player _player;
        [SerializeField] private UIDocument _PauseMenu;
        [SerializeField] private UIDocument _ShopMenu;
        [SerializeField] private GameResourceGUI[] resourcesGUI;
        [SerializeField] private VisualTreeAsset _gameResource;
        private Button _pauseButton;
        private Button _shopButton;

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            UpdateCounts();
            var root = _document.rootVisualElement;
            root.dataSource = resourcesGUI;
            _pauseButton = root.Q<Button>("PauseButton");
            _shopButton = root.Q<Button>("ShopButton");

            var resources = root.Q<VisualElement>("ResourceView");
            for (int i = 0; i < resourcesGUI.Length; i++)
            {
                VisualElement element = _gameResource.Instantiate();
                resources.Add(element);
                element.dataSourcePath = PropertyPath.FromIndex(i);
                element.Q<VisualElement>("Image").style.backgroundImage = resourcesGUI[i].Icon;
            }
            _pauseButton.clicked += Pause;
            _shopButton.clicked += OpenShop;
            _player.OnResourcesChanged += UpdateCounts;
        }

        private void UpdateCounts()
        {
            print("updating");
            var resp = _player.ResourcesCount;
            foreach(var res in resourcesGUI)
            {
                if (resp.TryGetValue(res.resource_id, out var count))
                {
                    res.Count = count;
                }
                else
                {
                    res.Count = 0;
                }
            }
        }

        private void OnDisable()
        {
            if (_pauseButton != null)
                _pauseButton.clicked -= Pause;
            if (_shopButton != null)
                _shopButton.clicked -= OpenShop;
        }

        private void Pause()
        {
            _PauseMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OpenShop()
        {
            _ShopMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    [Serializable]
    public class GameResourceGUI
    {
        public Texture2D Icon;
        public string resource_id;
        public int Count;
    }
}
