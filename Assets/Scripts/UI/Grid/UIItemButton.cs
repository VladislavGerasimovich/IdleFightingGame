using Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Grid
{
    [RequireComponent(typeof(Button))]
    public class UIItemButton : MonoBehaviour
    {
        private Button _button;

        public string Type { get; private set; }

        public event Action<string, UIItemButton> Click;
        public event Action<UIItemButton> ButtonDisabled;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            ButtonDisabled?.Invoke(this);
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public void Init(string type)
        {
            Type = type;
        }

        private void OnButtonClick()
        {
            Click?.Invoke(Type, this);
        }
    }
}