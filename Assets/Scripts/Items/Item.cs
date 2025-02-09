using UnityEngine;

namespace Items
{
    public class Item : ScriptableObject
    {
        [SerializeField] protected int MaxAmount;
        [SerializeField] protected int CurrentAmount;
        [SerializeField] private string _label;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _type;

        public string Label => _label;
        public Sprite Icon => _icon;
        public int MaxCount => MaxAmount;
        public int CurrentCount => CurrentAmount;
        public string Type => _type;

        public void SubtractAmount(int amount)
        {
            CurrentAmount -= amount;
        }

        public void AddAmount(int amount)
        {
            CurrentAmount += amount;
        }

        public void SetOriginalMeaning()
        {
            CurrentAmount = MaxAmount;
        }
    }
}