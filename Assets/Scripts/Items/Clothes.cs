using UnityEngine;

namespace Items 
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Clothes", order = 51)]
    public class Clothes : Item
    {
        [SerializeField] private int _armorCount;
        [SerializeField] private int _weightCount;

        public int ArmorCount => _armorCount;
        public int WeightCount => _weightCount;
    }
}