using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class Player_cursor : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private World world;

        private void Start()
        {
            input.actions.FindAction("Select").started += SelectField;
        }

        private void SelectField(InputAction.CallbackContext obj)
        {
             
        }
    }
}