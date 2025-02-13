using Health;
using UI;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private DressedItem _dressedHat;
        [SerializeField] private DressedItem _dressedBody;
        [SerializeField] private CharacterHealth _playerHealth;

        private bool _isTimeToBodyAttack;
        private int _maxDamage;
        private int _currentDamage;

        private void Awake()
        {
            _maxDamage = Constants.MaxEnemyDamage;
            _currentDamage = _maxDamage;
        }

        public void Run()
        {
            SetDamage();
            _playerHealth.TakeDamage(_currentDamage);
            _currentDamage = _maxDamage;
        }

        private void SetDamage()
        {
            _isTimeToBodyAttack = !_isTimeToBodyAttack;

            if (_isTimeToBodyAttack == false)
            {
                _currentDamage -= _dressedHat.ArmorCount;

                return;
            }

            _currentDamage -= _dressedBody.ArmorCount;
        }
    }
}