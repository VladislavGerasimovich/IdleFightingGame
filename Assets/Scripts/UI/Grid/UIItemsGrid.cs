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

        private GridLayoutGroup _gridLayoutGroup;
        private UIItemsUsed _itemsUsed;
        private int _count;
        private List<SlotStatus> _slots;

        private void Awake()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _itemsUsed = GetComponent<UIItemsUsed>();
            _count = 30;
            _slots = new List<SlotStatus>();

            for (int i = 0; i < _count; i++)
            {
                SlotStatus slot = Instantiate(_slotPrefab, _container);
                _slots.Add(slot);
            }

            for (int i = 0; i < _itemsUsed.Count; i++)
            {
                SlotStatus slot = _slots.Find(slot => slot.IsBusy == false);
                UIItemView itemView = Instantiate(_uiItemPrefab, slot.transform);
                Item item = _itemsUsed.Get(i);
                itemView.Set(item.Icon, item.Amount);
                slot.SetStatus(true);
            }
        }

        private void Start()
        {
            StartCoroutine(SwitchOffGridLayoutGroup());
        }

        private IEnumerator SwitchOffGridLayoutGroup()
        {
            yield return null;
            _gridLayoutGroup.enabled = false;
        }
    }
}