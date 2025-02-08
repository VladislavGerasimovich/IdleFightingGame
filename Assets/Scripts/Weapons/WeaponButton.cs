using System;
using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(WeaponStatus))]
    public class WeaponButton : MonoBehaviour
    {
        private WeaponStatus _weaponStatus;
        private Button _button;

        public event Action<WeaponStatus> StatusChanged;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _weaponStatus = GetComponent<WeaponStatus>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            StatusChanged?.Invoke(_weaponStatus);
        }
    }
}