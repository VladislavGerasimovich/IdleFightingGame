using UnityEngine;

namespace UI.Grid
{
    public class SlotStatus : MonoBehaviour
    {
        public bool IsBusy { get; private set; }

        public void Set(bool value)
        {
            IsBusy = value;
        }
    }
}