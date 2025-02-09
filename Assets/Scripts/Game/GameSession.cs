using Enemy;
using Health;
using Items;
using UI;
using UI.Grid;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace Game
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private Button _attackButton;
        [SerializeField] private WeaponButtons _weaponButtons;
        [SerializeField] private CharacterHealth _enemyHealth;
        [SerializeField] private UIItemsGrid _itemsGrid;
        [SerializeField] private UIItemsUsed _itemsUsed;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private CharacterHealth _playerHealth;
        [SerializeField] private GameOverWindow _gameOverWindow;
        [SerializeField] private WeaponAmmo _pistolAmmo;
        [SerializeField] private WeaponAmmo _rifleAmmo;

        private void OnEnable()
        {
            _attackButton.onClick.AddListener(OnAttackButtonClick);
        }

        private void OnDisable()
        {
            _attackButton.onClick.RemoveListener(OnAttackButtonClick);
        }

        private void OnAttackButtonClick()
        {
            WeaponStatus weaponStatus = _weaponButtons.GetReadyWeapon();

            if (weaponStatus != null)
            {
                WeaponAmmo weaponAmmo = weaponStatus.WeaponAmmo;
                WeaponInfo weaponInfo = weaponStatus.WeaponInfo;

                if (weaponAmmo.Count >= weaponInfo.CountOfShots)
                {
                    int damage = weaponInfo.Damage * weaponInfo.CountOfShots;
                    weaponAmmo.Subtract(weaponInfo.CountOfShots);
                    _enemyHealth.Set(damage);

                    if (_enemyHealth.Amount <= 0)
                    {
                        _enemyHealth.Restore();
                        Item item = _itemsUsed.GetRandomType();
                        _itemsGrid.AddItem(item.Icon, item.MaxCount, item.CurrentCount, item.Type);
                    }
                }
            }

            _enemyAttack.Run();

            if (_playerHealth.Amount <= 0)
            {
                _pistolAmmo.ResetCount();
                _rifleAmmo.ResetCount();
                _playerHealth.Restore();
                _enemyHealth.Restore();
                _gameOverWindow.Open();
            }
        }
    }
}