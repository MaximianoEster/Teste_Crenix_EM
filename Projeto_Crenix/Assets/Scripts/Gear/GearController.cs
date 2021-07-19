using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : DragDropItem, IDragItem
{
    public delegate void GearHandler(bool isOnCorrectPosition);
    public event GearHandler OnGearPositioned;
    
    [Header("Gear Settings")]
    [SerializeField] private Animator _animator = default;
    
    private GearMovementDirection _gearMovementDirection = default;
    
    public void DetectSlotType(SlotType currentSlotType)
    {
        switch (currentSlotType)
        {
            case SlotType.INTENTORY_SLOT:
                _isOnCorretPosition = false;
                break;
            
            case SlotType.GEAR_SLOT:
                _isOnCorretPosition = true;
                OnGearPositioned?.Invoke(_isOnCorretPosition);
                break;
        }
    }

    public void UpdateGearSettings(RectTransform newPos, GearMovementDirection gearMovementDirection)
    {
        _gearMovementDirection = gearMovementDirection;
        _rectTransform.position = newPos.position;
    }

    public void UpdateMovement(bool canRotate)
    {
        if (canRotate)
        {
            CheckMovementDirection();
        }
        else
        {
            _animator.SetTrigger(AnimationClipsNames.Idle);
        }
    }
    
    public void ResetPosition(Vector3 pos)
    {
        _rectTransform.position = pos;
        _isOnCorretPosition = false;
    }

    private void CheckMovementDirection()
    {
        switch (_gearMovementDirection)
        {
            case GearMovementDirection.LEFT:
                _animator.SetTrigger(AnimationClipsNames.LeftMovement);
                break;
            
            case GearMovementDirection.RIGHT:
                _animator.SetTrigger(AnimationClipsNames.RightMovement);
                break;
        }
    }
}
