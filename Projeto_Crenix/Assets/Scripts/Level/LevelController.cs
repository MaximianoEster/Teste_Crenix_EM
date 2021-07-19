using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Gear List")]
    [SerializeField] private List<GearController> _gearList = new List<GearController>();
    
    [Space, Header("Slot List")]
    [SerializeField] private List<SlotController> _emptySlotList = new List<SlotController>();
    [SerializeField] private List<SlotController> _inventorySlotList = new List<SlotController>();
    
    [Space, Header("Dialogue System")]
    [SerializeField] private TMP_Text _nuggetDialogueText = default;
    [SerializeField] private DialogueData _dialogueData = default;
    
    private string _currentDialogueText = String.Empty;
        
    private int _gearCorrectPosCount = 0;
    private int _gearTotalSequenceCount = default;
    private bool _canStartGearsRotation = false;
    
    private void Start()
    {
        InitializePuzzleSystem();
    }
    
    public void ResetPuzzle()
    {
        for (int i = 0; i < _gearList.Count; i++)
        {
            _gearList[i].ResetPosition(_inventorySlotList[i].OnResetSlot());
        }

        _gearCorrectPosCount = 0;
        CheckGearSequence();
    }

    public void CheckGearPosition(bool isOnCorrectPosition)
    {
        if (isOnCorrectPosition)
        {
            _gearCorrectPosCount++;
        }
        else
        {
            _gearCorrectPosCount--;
        }
        
        CheckGearSequence();
    }

    private void CheckGearSequence()
    {
        if (_gearCorrectPosCount == _gearTotalSequenceCount)
        {
            _canStartGearsRotation = true;
            _currentDialogueText = _dialogueData.CompleteTask;
        }
        else
        {
            _canStartGearsRotation = false;
            _currentDialogueText = _dialogueData.IncompleteTask;
        }

        _nuggetDialogueText.text = _currentDialogueText;
        UpdateGearsMovement(_canStartGearsRotation);
    }

    private void UpdateGearsMovement(bool canRotate)
    {
        for (int i = 0; i < _gearList.Count; i++)
        {
            _gearList[i].UpdateMovement(canRotate );
        }
    }
    
    private void InitializePuzzleSystem()
    {
        _gearTotalSequenceCount = _gearList.Count;
        
        InitializeAllSlots();
        SubscribeOnGearEvents();
        ResetPuzzle();
    }

    private void InitializeAllSlots()
    {
        for (int i = 0; i < _gearTotalSequenceCount; i++)
        {
            _emptySlotList[i].InitializeSlot();
            _inventorySlotList[i].InitializeSlot();
        }
    }

    private void SubscribeOnGearEvents()
    {
        for (int i = 0; i < _gearList.Count; i++)
        {
            _gearList[i].OnGearPositioned += CheckGearPosition;
            _gearList[i].ItemStartDrag += CheckGearPosition;
        }
    }
}
