using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    [RequireComponent(typeof(HealthView))]
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] protected int Count;
        [SerializeField] protected float MaxCount;

        protected HealthView HealthView;

        public int Amount => Count;

        private void Awake()
        {
            HealthView = GetComponent<HealthView>();
            Count = (int)MaxCount;
            HealthView.Set(Count, MaxCount);
        }

        public void Set(int value)
        {
            Count -= value;

            if(Count < 0)
            {
                Count = 0;
            }

            HealthView.Set(Count, MaxCount);
        }

        public void Restore()
        {
            Count = (int)MaxCount;
            HealthView.Set(Count, MaxCount);
        }
    }
}