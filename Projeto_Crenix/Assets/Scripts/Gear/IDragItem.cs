using UnityEngine;

interface IDragItem
{
    void DetectSlotType(SlotType slotType);
    void UpdateGearSettings(RectTransform newPos,GearMovementDirection dir );
}