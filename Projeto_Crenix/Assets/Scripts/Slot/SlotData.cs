using UnityEngine;

[CreateAssetMenu(fileName = "Slot Data", menuName = "Scriptable Object/Data/Slot Data")]
public class SlotData : ScriptableObject
{
    public SlotType SlotType;
    public GearMovementDirection GearMovementDirection;
}
