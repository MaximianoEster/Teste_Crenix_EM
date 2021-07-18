using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public delegate void DragHandler(bool onCorrectPosition);
    public event DragHandler OnItemDrag;
    
    [Header("Drag and Drop Settings")]
    [SerializeField] protected RectTransform _rectTransform = default;
    [SerializeField] private Canvas _canvas = default;
    [SerializeField] private CanvasGroup _canvasGroup = default;
    
    protected bool _isOnCorretPosition = false;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        
        if (_isOnCorretPosition)
        {
            _isOnCorretPosition = false;
            OnItemDrag?.Invoke(_isOnCorretPosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }
    
}
