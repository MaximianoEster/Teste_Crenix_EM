using UnityEngine;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour,IDropHandler
{
    [Header("Slot Settings")]
    [SerializeField] private RectTransform _rectTransformSlot = default;
    [SerializeField] private SlotData _slotData = default;
    
    private SlotType _slotType = default;
    private GearMovementDirection _gearGearMovementDirection = default;
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.TryGetComponent(out IDragItem item))
            {
                item.UpdateGearSettings(_rectTransformSlot, _gearGearMovementDirection);
                item.DetectSlotType(_slotType);
            }
        }
    }

    public void InitializeSlot()
    {
        _slotType = _slotData.SlotType;
        _gearGearMovementDirection = _slotData.GearMovementDirection;
    }

    public Vector3 OnResetSlot()
    {
        return _rectTransformSlot.position;
    }
}
