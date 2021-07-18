using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<GearController> _gearList = new List<GearController>();
    [SerializeField] private List<SlotController> _slotControllerList = new List<SlotController>();
    
    [SerializeField] private TMP_Text _nuggetDialogueText = default;
    [SerializeField] private DialogueData _dialogueData = default;
    
    private string _currentDialogueText = String.Empty;
        
    private int _gearCorrectPosAmount = 0;
    private int _gearSequenceAmount = default;
    private bool _canStartGearsRotation = false;
    
    private void Start()
    {
        InitializePuzzleSystem();
    }
    
    private void InitializePuzzleSystem()
    {
        _gearSequenceAmount = _gearList.Count;
        ResetPuzzle();
       
        for (int i = 0; i < _gearList.Count; i++)
        {
            _gearList[i].OnGearPositioned += CheckGearPosition;
            _gearList[i].OnItemDrag += CheckGearPosition;
        }
    }

    public void ResetPuzzle()
    {
        for (int i = 0; i < _gearList.Count; i++)
        {
            _gearList[i].ResetPosition(_slotControllerList[i].OnResetSlot());
        }

        _gearCorrectPosAmount = 0;
        CheckGearSequence();
    }

    public void CheckGearPosition(bool isOnCorrectPosition)
    {
        if (isOnCorrectPosition)
        {
            _gearCorrectPosAmount++;
        }
        else
        {
            _gearCorrectPosAmount--;
        }
        Debug.Log("Sequence => "+ _gearCorrectPosAmount);
        CheckGearSequence();
    }

    private void CheckGearSequence()
    {
        if (_gearCorrectPosAmount == _gearSequenceAmount)
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
        Debug.Log(_currentDialogueText);
        UpdateGearsMovement(_canStartGearsRotation);
    }

    private void UpdateGearsMovement(bool canRotate)
    {
        for (int i = 0; i < _gearList.Count; i++)
        {
            _gearList[i].UpdateMovement(canRotate );
        }
    }
}
