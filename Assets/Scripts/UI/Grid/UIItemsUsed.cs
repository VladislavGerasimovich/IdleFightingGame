using System.Collections.Generic;
using Items;
using UnityEngine;

namespace UI.Grid
{
    public class UIItemsUsed : MonoBehaviour
    {
        [SerializeField] private List<Item> _items;

        public int Count => _items.Count;

        public Item Get(int index)
        {
            return _items[index];
        }
    }
}