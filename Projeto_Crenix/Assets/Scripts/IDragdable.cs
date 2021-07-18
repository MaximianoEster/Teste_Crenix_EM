using UnityEngine;

interface IDragdable
{
    void DetectSlot(SlotType slotType);
    void UpdatePosition(RectTransform newPos);
    void UpdateMovementDirection(GearMovementDirection dir);
}