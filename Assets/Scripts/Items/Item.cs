using UnityEngine;

namespace Items
{
    public class Item : ScriptableObject
    {
        [SerializeField] private string _label;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _amount;
        [SerializeField] private string _type;

        public string Label => _label;
        public Sprite Icon => _icon;
        public int Amount => _amount;
        public string Type => _type;
    }
}