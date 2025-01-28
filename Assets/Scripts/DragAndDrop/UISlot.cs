using UI.Grid;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SlotStatus))]
public class UISlot : MonoBehaviour, IDropHandler
{
    private SlotStatus _slotStatus;

    private void Awake()
    {
        _slotStatus = GetComponent<SlotStatus>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(_slotStatus.IsBusy == false)
        {
            _slotStatus.SetStatus(true);
            Transform otherItemTransform = eventData.pointerDrag.transform;
            SlotStatus slotStatus = otherItemTransform.GetComponentInParent<SlotStatus>();
            slotStatus.SetStatus(false);
            otherItemTransform.SetParent(transform);
            otherItemTransform.localPosition = Vector3.zero;
        }
    }
}