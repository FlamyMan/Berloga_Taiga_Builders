using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private UIDocument Pause;

    private UIDocument doc;
    private Slider sliderMaster;
    private Slider SFX;
    private Slider Music;
    private Button BBack;

    private float _prMaster;
    private float _prSFX;
    private float _prMusic;

    private void Awake()
    {
        doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        sliderMaster = doc.rootVisualElement.Q<Slider>("MasterSound");
        SFX = doc.rootVisualElement.Q<Slider>("SFXSound");
        Music = doc.rootVisualElement.Q<Slider>("MusicSound");
        BBack = doc.rootVisualElement.Q<Button>("BackButton");
        BBack.clicked += Back;
    }

    private void Back()
    {
        Pause.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Update()
    {
        if (!Mathf.Approximately(sliderMaster.value, _prMaster))
        {
            audioMixer.SetFloat("MasterVol", sliderMaster.value);
            _prMaster = sliderMaster.value;
        }
        if (!Mathf.Approximately(SFX.value, _prSFX))
        {
            audioMixer.SetFloat("SFXVol", SFX.value);
            _prSFX = SFX.value;
        }
        if (!Mathf.Approximately(Music.value, _prMusic))
        {
            audioMixer.SetFloat("MusicVol", Music.value);
            _prMusic = Music.value;
        }
    }
}