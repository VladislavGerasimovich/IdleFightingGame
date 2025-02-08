using UnityEngine;

namespace Items
{
    public class Item : ScriptableObject
    {
        [SerializeField] private string _label;
        [SerializeField] private Sprite _icon;
        [SerializeField] protected int _maxAmount;
        [SerializeField] protected int _currentAmount;
        [SerializeField] private string _type;

        public string Label => _label;
        public Sprite Icon => _icon;
        public int MaxAmount => _maxAmount;
        public int CurrentAmount => _currentAmount;
        public string Type => _type;

        public void SubtractAmount(int amount)
        {
            _currentAmount -= amount;
        }

        public void AddAmount(int amount)
        {
            _currentAmount += amount;
        }

        public void Delete()
        {
            _currentAmount = 0;
        }
    }
}