using UnityEngine;

namespace Weapons
{
    public class WeaponInfo : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _countOfShots;

        public int Damage => _damage;
        public int CountOfShots => _countOfShots;
    }
}