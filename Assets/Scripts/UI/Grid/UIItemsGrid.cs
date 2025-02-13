using Interfaces;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Grid
{
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(UIItemsUsed))]
    public class UIItemsGrid : MonoBehaviour
    {
        [SerializeField] private SlotStatus _slotPrefab;
        [SerializeField] private UIItemView _uiItemPrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private InfoWindow _popUpWindow;

        private GridLayoutGroup _gridLayoutGroup;
        private UIItemsUsed _itemsUsed;
        private int _count;
        private List<SlotStatus> _slots;
        private List<UIItemView> _itemsView;

        private void Awake()
        {
            _itemsView = new List<UIItemView>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _itemsUsed = GetComponent<UIItemsUsed>();
            _count = 30;
            _slots = new List<SlotStatus>();

            for (int i = 0; i < _count; i++)
            {
                SlotStatus slot = Instantiate(_slotPrefab, _container);
                _slots.Add(slot);
            }

            Set();
        }

        private void OnEnable()
        {
            _popUpWindow.OnDeleteButtonClicked += DeleteItem;
            _popUpWindow.OnUseButtonClicked += OnUseButtonClicked;
        }

        private void Start()
        {
            StartCoroutine(SwitchOffGridLayoutGroup());
        }

        private void OnDisable()
        {
            _popUpWindow.OnDeleteButtonClicked -= DeleteItem;
            _popUpWindow.OnUseButtonClicked -= OnUseButtonClicked;
        }
        
        public void AddItem(Sprite icon, int maxAmount, int currentAmount, string type)
        {
            int amount = currentAmount;

            foreach (UIItemView itemView in _itemsView)
            {
                UIItemButton itemButton = itemView.GetComponent<UIItemButton>();

                if (itemButton.Type == type)
                {
                    if(amount <= maxAmount)
                    {
                        itemView.Set(icon, amount);
                        amount = 0;
                    }

                    amount -= itemView.Amount;
                }
            }

            while (amount > 0)
            {
                CreateItem(out UIItemView itemView);

                if(amount < maxAmount)
                {
                    itemView.Set(icon, amount);
                    InitButton(itemView, type);
                    amount -= maxAmount;

                    return;
                }

                itemView.Set(icon, maxAmount);
                InitButton(itemView, type);
                amount -= maxAmount;
            }
        }

        public void Set()
        {
            for (int i = 0; i < _itemsUsed.ConsumablesCount; i++)
            {
                Consumables item = _itemsUsed.GetConsumablesByIndex(i);
                AddItem(item.Icon, item.MaxCount, item.CurrentCount, item.Type);
            }

            for (int i = 0; i < _itemsUsed.ClothesCount; i++)
            {
                Clothes item = _itemsUsed.GetClothesByIndex(i);
                AddItem(item.Icon, item.MaxCount, item.CurrentCount, item.Type);
            }
        }

        public void DeleteAllItems()
        {
            foreach (UIItemView itemView in _itemsView)
            {
                Destroy(itemView.gameObject);
            }

            foreach (SlotStatus slotStatus in _slots)
            {
                slotStatus.Set(false);
            }

            _itemsView.Clear();
        }

        private void OnUseButtonClicked(string type)
        {
            if (type == Constants.FirstAid || type == Constants.PistolAmmo || type == Constants.RifleAmmo)
            {
                Consumables consumables = _itemsUsed.GetConsumables(type);
                AddItem(consumables.Icon, consumables.MaxCount, consumables.CurrentCount, consumables.Type);

                return;
            }

            Clothes clothes = _itemsUsed.GetClothes(type);

            if(clothes != null)
            {
                AddItem(clothes.Icon, clothes.MaxCount, clothes.CurrentCount, clothes.Type);
            }
        }

        private IEnumerator SwitchOffGridLayoutGroup()
        {
            yield return null;
            _gridLayoutGroup.enabled = false;
        }

        private void InitButton(UIItemView itemView, string type)
        {
            UIItemButton itemButton = itemView.GetComponent<UIItemButton>();
            itemButton.Init(type);
            itemButton.Click += OnItemButtonClick;
            itemButton.ButtonDisabled += OnButtonDisabled;
        }

        private void CreateItem(out UIItemView itemView)
        {
            SlotStatus slot = _slots.Find(slot => slot.IsBusy == false);
            itemView = Instantiate(_uiItemPrefab, slot.transform);
            _itemsView.Add(itemView);
            slot.Set(true);
        }

        private void DeleteItem(UIItemView itemView)
        {
            UIItemButton itemButton = itemView.GetComponent<UIItemButton>();
            Clothes clothes = _itemsUsed.GetClothes(itemButton.Type);

            if(clothes != null)
            {
                clothes.SubtractAmount(itemView.Amount);
            }

            _itemsView.Remove(itemView);
            SlotStatus slotStatus = itemButton.GetComponentInParent<SlotStatus>();
            slotStatus.Set(false);
            Destroy(itemButton.gameObject);
        }

        private void OnButtonDisabled(IItemButton itemButton)
        {
            itemButton.Click -= OnItemButtonClick;
            itemButton.ButtonDisabled -= OnButtonDisabled;
        }

        private void OnItemButtonClick(IItemButton itemButton)
        {
            if (itemButton.Type == Constants.FirstAid || itemButton.Type == Constants.PistolAmmo || itemButton.Type == Constants.RifleAmmo)
            {
                Consumables consumables = _itemsUsed.GetConsumables(itemButton.Type);
                _popUpWindow.Set(
                    itemButton.ItemView,
                    consumables.Label,
                    consumables.Icon,
                    consumables.Type,
                    consumables.AmountToSell,
                    0,
                    0,
                    consumables.Info);

                return;
            }

            Clothes clothes = _itemsUsed.GetClothes(itemButton.Type);
            _popUpWindow.Set(
                itemButton.ItemView,
                clothes.Label,
                clothes.Icon,
                clothes.Type,
                0,
                clothes.ArmorCount,
                clothes.WeightCount);
        }
    }
}