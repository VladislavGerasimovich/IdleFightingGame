using Items;
using UI.Grid;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class GameSession : MonoBehaviour
{
    [SerializeField] private Button _attackButton;
    [SerializeField] private WeaponButtons _weaponButtons;
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private UIItemsGrid _itemsGrid;
    [SerializeField] private UIItemsUsed _itemsUsed;

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

        if(weaponStatus != null)
        {
            WeaponAmmo weaponAmmo = weaponStatus.WeaponAmmo;
            WeaponInfo weaponInfo = weaponStatus.WeaponInfo;

            if(weaponAmmo.Count >= weaponInfo.CountOfShots)
            {
                int damage = weaponInfo.Damage * weaponInfo.CountOfShots;
                weaponAmmo.Subtract(weaponInfo.CountOfShots);
                _enemyHealth.Set(damage);

                if(_enemyHealth.Count <= 0)
                {
                    _enemyHealth.Restore();
                    Item item = _itemsUsed.GetRandomType();
                    _itemsGrid.AddItem(item.Icon, item.MaxAmount, item.CurrentAmount, item.Type);
                }
            }
        }
    }
}