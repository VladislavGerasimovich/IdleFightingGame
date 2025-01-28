using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Consumable", order = 51)]
    public class Consumables : Item
    {
        [SerializeField] private int _cost;

        public int Cost => _cost;
    }
}