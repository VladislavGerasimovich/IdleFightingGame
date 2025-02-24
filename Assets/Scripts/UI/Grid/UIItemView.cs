using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Grid
{
    public class UIItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _text;

        private int _minValue;

        public int Amount { get; private set; }

        private void Awake()
        {
            _minValue = 1;
        }

        public void Set(Sprite sprite, int amount)
        {
            _icon.sprite = sprite;
            _text.text = amount.ToString();
            Amount = amount;

            if (amount <= _minValue)
            {
                _text.enabled = false;

                return;
            }

            _text.enabled = true;
        }
    }
}