using Assets.Scripts;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseGUI : MonoBehaviour
{
    private UIDocument doc;
    [SerializeField] private UIDocument _mainUI;
    [SerializeField] private UIDocument _settings;
    [SerializeField] private World _world;
    private Button ContinueB;
    private Button SettingsB;
    private Button QuitB;

    private void Awake()
    {
        doc = GetComponent<UIDocument>();
    }
    private void OnEnable()
    {
        ContinueB = doc.rootVisualElement.Q<Button>("ContinueButton");
        SettingsB = doc.rootVisualElement.Q<Button>("SettingsButton");
        QuitB = doc.rootVisualElement.Q<Button>("ExitButton");
        ContinueB.clicked += ContinueGame;
        SettingsB.clicked += OpenSettings;
        QuitB.clicked += QuitGame;
    }

    private void OnDisable()
    {
        if(ContinueB != null) ContinueB.clicked -= ContinueGame;
        if (SettingsB != null) SettingsB.clicked -= OpenSettings;
        if (QuitB != null) QuitB.clicked -= QuitGame;
    }

    private void QuitGame()
    {
        //TODO: Save World Before Quit
        Application.Quit();
    }

    private void OpenSettings()
    {
        _settings.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ContinueGame()
    {
        _mainUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
