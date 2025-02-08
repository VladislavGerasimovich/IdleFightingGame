using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponButtons : MonoBehaviour
    {
        [SerializeField] private List<WeaponButton> _weaponsButtons;
        [SerializeField] private List<WeaponStatus> _weaponsStatus;

        private void OnEnable()
        {
            foreach (WeaponButton button in _weaponsButtons)
            {
                button.StatusChanged += StatusChanged;
            }
        }

        private void OnDisable()
        {
            foreach (WeaponButton button in _weaponsButtons)
            {
                button.StatusChanged -= StatusChanged;
            }
        }

        public WeaponStatus GetReadyWeapon()
        {
            foreach (WeaponStatus weapon in _weaponsStatus)
            {
                if(weapon.IsReady == true)
                {
                    return weapon;
                }
            }

            return null;
        }

        private void StatusChanged(WeaponStatus weaponStatus)
        {
            foreach (WeaponStatus status in _weaponsStatus)
            {
                status.Set(false);
            }

            _weaponsStatus.Find(status => status == weaponStatus).Set(true);
        }
    }
}