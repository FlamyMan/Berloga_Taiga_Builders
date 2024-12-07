using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class MainGUI : MonoBehaviour
    {
        [SerializeField] private UIDocument _document;

        private Button button;

        private void Awake()
        {
            _document = GetComponent<UIDocument>();

            button = _document.rootVisualElement.Q("");
        }
    }
}
