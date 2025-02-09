using Health;
using Items;
using System;
using TMPro;
using UI.Grid;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class InfoWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Image _icon;
        [SerializeField] private Transform _infoForClothes;
        [SerializeField] private TMP_Text _armorText;
        [SerializeField] private TMP_Text _weightText;
        [SerializeField] private TMP_Text _infoForConsumablesText;
        [SerializeField] private Transform _infoForConsumableItems;
        [SerializeField] private TMP_Text _useButtonText;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _useButton;
        [SerializeField] private UIItemsUsed _itemsUsed;
        [SerializeField] private DressedItem _dressedJacket;
        [SerializeField] private DressedItem _dressedHat;
        [SerializeField] private WeaponAmmo _pistolWeapon;
        [SerializeField] private WeaponAmmo _rifleWeapon;
        [SerializeField] private PlayerHealth _playerHealth;

        private UIItemButton _itemButton;
        private Sprite _itemIcon;
        private string _type;
        private int _armor;
        private int _amountToSell;

        public event Action<UIItemButton> OnDeleteButtonClicked;
        public event Action<string> OnUseButtonClicked;

        private void OnEnable()
        {
            _deleteButton.onClick.AddListener(OnDeleteButtonClick);
            _useButton.onClick.AddListener(OnUseButtonClick);
        }

        private void OnDisable()
        {
            _deleteButton.onClick.RemoveListener(OnDeleteButtonClick);
            _useButton.onClick.RemoveListener(OnUseButtonClick);
        }

        public void Set(
            UIItemButton itemButton,
            string label,
            Sprite icon,
            string type,
            int amountToSell = 0,
            int armor = 0,
            int weight = 0,
            string info = "")
        {
            _itemButton = itemButton;
            _type = type;
            transform.gameObject.SetActive(true);
            _label.text = label;
            _icon.sprite = icon;
            _itemIcon = icon;
            _amountToSell = amountToSell;
            _armor = armor;
            UIItemView itemView = _itemButton.GetComponent<UIItemView>();

            if(itemView.Amount < _amountToSell)
            {
                _amountToSell = itemView.Amount;
            }

            if (type == Constants.FirstAid)
            {
                SetBuyButtonText(Constants.Heal);
                SetInfoForConsumables(_amountToSell, info);
            }
            else if (type == Constants.PistolAmmo || type == Constants.RifleAmmo)
            {
                SetBuyButtonText(Constants.Buy);
                SetInfoForConsumables(_amountToSell, info);
            }
            else if (type == Constants.Jacket || type == Constants.BulletProofVest)
            {
                SetBuyButtonText(Constants.Equip);
                SetInfoForClothes(armor, weight);
            }
            else if (type == Constants.Cap || type == Constants.Helmet)
            {
                SetBuyButtonText(Constants.Equip);
                SetInfoForClothes(armor, weight);
            }
        }

        private void OnUseButtonClick()
        {
            if (_type == Constants.Jacket || _type == Constants.BulletProofVest)
            {
                SetClothes(_dressedJacket);
            }
            else if (_type == Constants.Cap || _type == Constants.Helmet)
            {
                SetClothes(_dressedHat);
            }
            else if (_type == Constants.PistolAmmo || _type == Constants.RifleAmmo)
            {
                bool isReload = false;

                if(_type == Constants.PistolAmmo)
                {
                    _pistolWeapon.Add(_amountToSell, out bool canSet);
                    isReload = canSet;
                }

                if (_type == Constants.RifleAmmo)
                {
                    _rifleWeapon.Add(_amountToSell, out bool canSet);
                    isReload = canSet;
                }

                if (isReload == true)
                {
                    SetConsumables();
                }

                Close();
            }
            else if(_type == Constants.FirstAid)
            {
                _playerHealth.Add(Constants.AmountRestoredHealth);
                SetConsumables();
            }
        }

        private void SetClothes(DressedItem dressedItem)
        {
            string type = dressedItem.Type;
            dressedItem.SetValues(_itemIcon, _label.text, _armor, _type);
            OnDeleteButtonClick();

            if (type != null)
            {
                _itemsUsed.AddItemByType(type, Constants.AmountAddedClothes);
                OnUseButtonClicked?.Invoke(type);
            }
        }

        private void SetConsumables()
        {
            Consumables consumables = _itemsUsed.GetConsumables(_type);
            UIItemView itemView = _itemButton.GetComponent<UIItemView>();
            consumables.SubtractAmount(_amountToSell);

            if (itemView.Amount - _amountToSell <= 0)
            {
                OnDeleteButtonClick();
                Close();

                return;
            }

            itemView.Set(_itemIcon, consumables.CurrentCount);
            Close();
        }

        private void OnDeleteButtonClick()
        {
            OnDeleteButtonClicked?.Invoke(_itemButton);
            Close();
        }

        private void SetBuyButtonText(string text)
        {
            _useButtonText.text = text;
        }

        private void SetInfoForConsumables(int cost, string info)
        {
            _infoForConsumableItems.gameObject.SetActive(true);

            if(_type == Constants.FirstAid)
            {
                _infoForConsumablesText.text = info;

                return;
            }

            _infoForConsumablesText.text = $"{info} {cost}";
        }

        private void SetInfoForClothes(int armor, int weight)
        {
            _infoForClothes.gameObject.SetActive(true);
            _armorText.text = armor.ToString();
            _weightText.text = weight.ToString();
        }

        private void Close()
        {
            transform.gameObject.SetActive(false);
            _infoForClothes.gameObject.SetActive(false);
            _infoForConsumableItems.gameObject.SetActive(false);
        }
    }
}