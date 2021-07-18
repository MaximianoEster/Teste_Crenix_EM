using UnityEngine;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour,IDropHandler
{
    
    [SerializeField] private RectTransform _rectTransformSlot = default;
    [SerializeField] private SlotData _slotData = default;
    
    private SlotType _slotType = default;
    private GearMovementDirection _gearGearMovementDirection = default;
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.TryGetComponent(out IDragdable item))
            {
                item.UpdatePosition(_rectTransformSlot);
                item.UpdateMovementDirection(_gearGearMovementDirection);
                item.DetectSlot(_slotType);
            }
        }
        else
        {
            Debug.Log("Null");
        }
    }

    private void Start()
    {
        InitializeSlot();
    }
    
    private void InitializeSlot()
    {
        _slotType = _slotData.SlotType;
        _gearGearMovementDirection = _slotData.GearMovementDirection;
    }

    public Vector3 OnResetSlot()
    {
        return _rectTransformSlot.position;
    }
}
