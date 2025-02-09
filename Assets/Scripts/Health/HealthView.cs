using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private TMP_Text _textCount;

        public void Set(int count, float maxCount)
        {
            _healthBar.fillAmount = count / maxCount;
            _textCount.text = count.ToString();
        }
    }
}