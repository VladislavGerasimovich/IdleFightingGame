using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    [RequireComponent(typeof(WeaponInfo))]
    [RequireComponent(typeof(WeaponAmmo))]
    public class WeaponStatus : MonoBehaviour
    {
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Image _image;

        public WeaponInfo WeaponInfo;
        public WeaponAmmo WeaponAmmo;

        public bool IsReady { get; private set; }

        private void Awake()
        {
            WeaponInfo = GetComponent<WeaponInfo>();
            WeaponAmmo = GetComponent<WeaponAmmo>();
        }

        public void Set(bool value)
        {
            IsReady = value;
            _image.color = value ? _selectedColor : _defaultColor;
        }
    }
}