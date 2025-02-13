using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DressedItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _armor;
        [SerializeField] private Sprite _originalSprite;
        [SerializeField] private string _originalLabel;
        [SerializeField] private int _originalArmorCount;

        private void Awake()
        {
            ResetValues();
        }

        public string Type { get; private set; }
        public int ArmorCount { get; private set; }

        public void SetValues(Sprite icon, string label, int armor, string newType)
        {
            Type = newType;
            _icon.sprite = icon;
            _label.text = label;
            _armor.text = armor.ToString();
            ArmorCount = armor;
        }

        public void ResetValues()
        {
            Type = "";
            _icon.sprite = _originalSprite;
            _label.text = _originalLabel;
            _armor.text = _originalArmorCount.ToString();
        }
    }
}