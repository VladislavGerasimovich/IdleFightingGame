using UnityEngine;

namespace UI.Grid
{
    public class SlotStatus : MonoBehaviour
    {
        public bool IsBusy { get; private set; }

        public void SetStatus(bool value)
        {
            IsBusy = value;
        }
    }
}