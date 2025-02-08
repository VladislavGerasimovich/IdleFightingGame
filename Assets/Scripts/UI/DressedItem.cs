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

        public string Type;

        public void SetValues(Sprite icon, string label, int armor, string newType)
        {
            Type = newType;
            _icon.sprite = icon;
            _label.text = label;
            _armor.text = armor.ToString();
        }
    }
}