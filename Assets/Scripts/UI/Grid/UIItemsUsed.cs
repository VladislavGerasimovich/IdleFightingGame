using System.Collections.Generic;
using Items;
using UnityEngine;
using Weapons;

namespace UI.Grid
{
    public class UIItemsUsed : MonoBehaviour
    {
        [SerializeField] private List<Consumables> _consumables;
        [SerializeField] private List<Clothes> _clothes;
        [SerializeField] private DressedItem _dressedJacket;
        [SerializeField] private DressedItem _dressedHat;
        [SerializeField] private WeaponAmmo _pistolAmmo;
        [SerializeField] private WeaponAmmo _rifleAmmo;

        private List<string> _allTypes;

        public int ConsumablesCount => _consumables.Count;
        public int ClothesCount => _clothes.Count;

        private void Awake()
        {
            _allTypes = new List<string>();

            foreach (Consumables consumables in _consumables)
            {
                _allTypes.Add(consumables.Type);
            }

            foreach (Clothes clothes in _clothes)
            {
                _allTypes.Add(clothes.Type);
            }
        }

        private void OnDisable()
        {
            if(_dressedJacket.Type != "")
            {
                AddItemByType(_dressedJacket.Type, Constants.AmountAddedClothes);
            }

            if (_dressedHat.Type != "")
            {
                AddItemByType(_dressedHat.Type, Constants.AmountAddedClothes);
            }

            if (_pistolAmmo.Count > 0)
            {
                AddItemByType(_pistolAmmo.Type, _pistolAmmo.Count);
            }

            if (_rifleAmmo.Count > 0)
            {
                AddItemByType(_rifleAmmo.Type, _rifleAmmo.Count);
            }
        }

        public void Set()
        {
            foreach (Consumables consumables in _consumables)
            {
                consumables.SetOriginalMeaning();
            }

            foreach (Clothes clothes in _clothes)
            {
                clothes.SetOriginalMeaning();
            }
        }

        public Item GetRandomType()
        {
            System.Random random = new System.Random();
            int index = random.Next(_allTypes.Count);
            string itemType = _allTypes[index];

            if (
                itemType == Constants.Jacket ||
                itemType == Constants.BulletProofVest ||
                itemType == Constants.Helmet ||
                itemType == Constants.Cap)
            {
                AddItemByType(itemType, Constants.AmountAddedClothes);
                return GetClothes(itemType);
            }
            else if (itemType == Constants.PistolAmmo)
            {
                AddItemByType(itemType, Constants.AmountAddedPistolAmmo);
                return GetConsumables(itemType);
            }
            else if (itemType == Constants.RifleAmmo)
            {
                AddItemByType(itemType, Constants.AmountAddedRifleAmmo);
                return GetConsumables(itemType);
            }
            else if (itemType == Constants.FirstAid)
            {
                AddItemByType(itemType, Constants.AmountAddedClothes);
                return GetConsumables(itemType);
            }

            return null;
        }

        public Consumables GetConsumablesByIndex(int index)
        {
            return _consumables[index];
        }

        public Clothes GetClothesByIndex(int index)
        {
            return _clothes[index];
        }

        public Consumables GetConsumables(string type)
        {
            foreach (Consumables consumables in _consumables)
            {
                if (consumables.Type == type)
                {
                    return consumables;
                }
            }

            return null;
        }

        public Clothes GetClothes(string type)
        {
            foreach (Clothes clothes in _clothes)
            {
                if(clothes.Type == type)
                {
                    return clothes;
                }
            }

            return null;
        }

        public void AddItemByType(string type, int count)
        {
            foreach (Consumables consumables in _consumables)
            {
                if (consumables.Type == type)
                {
                    consumables.AddAmount(count);

                    return;
                }
            }

            foreach (Clothes clothes in _clothes)
            {
                if (clothes.Type == type)
                {
                    clothes.AddAmount(count);

                    return;
                }
            }
        }
    }
}