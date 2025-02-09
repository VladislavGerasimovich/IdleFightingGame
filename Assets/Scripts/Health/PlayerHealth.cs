using UnityEngine;

namespace Health
{
    public class PlayerHealth : CharacterHealth
    {
        public void Add(int value)
        {
            Count += value;

            if(Count > MaxCount)
            {
                Count = (int)MaxCount;
            }

            HealthView.Set(Count, MaxCount);
        }
    }
}