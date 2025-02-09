using TMPro;
using UnityEngine;

namespace Weapons
{
    public class WeaponAmmo : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private int _maxCount;
        [SerializeField] private string _type;

        public int Count { get; private set; }
        public string Type => _type;

        public void Add(int count, out bool canSet)
        {
            int _tempCount = Count;
        
            if(_tempCount + count <= _maxCount)
            {
                Count += count;
                _countText.text = Count.ToString();
                canSet = true;

                return;
            }

            canSet = false;
        }

        public void Subtract(int count)
        {
            Count -= count;
            _countText.text = Count.ToString();
        }

        public void ResetCount()
        {
            Count = 0;
            _countText.text = Count.ToString();
        }
    }
}