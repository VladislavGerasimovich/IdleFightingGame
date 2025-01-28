using UnityEngine;

namespace Items 
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Clothes", order = 51)]
    public class Clothes : Item
    {
        [SerializeField] private Sprite _armorIcon;
        [SerializeField] private int _armorCount;
        [SerializeField] private Sprite _weightIcon;
        [SerializeField] private int _weightCount;

        public Sprite ArmorIcon => _armorIcon;
        public int ArmorCount => _armorCount;
        public Sprite WeightIcon => _weightIcon;
        public int WeightCount => _weightCount;
    }
}