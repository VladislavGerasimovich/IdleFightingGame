using Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Grid
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(UIItemView))]
    public class UIItemButton : MonoBehaviour, IItemButton
    {
        private Button _button;

        public event Action<IItemButton> Click;
        public event Action<IItemButton> ButtonDisabled;

        public UIItemView ItemView { get; private set; }
        public string Type { get; private set; }

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
            ItemView = GetComponent<UIItemView>();
            Type = type;
        }

        private void OnButtonClick()
        {
            Click?.Invoke(this);
        }
    }
}