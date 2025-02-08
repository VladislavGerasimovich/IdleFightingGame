using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Consumable", order = 51)]
    public class Consumables : Item
    {
        [SerializeField] private int _amountToSell;
        [SerializeField] private string _info;

        public int AmountToSell => _amountToSell;
        public string Info => _info;
    }
}