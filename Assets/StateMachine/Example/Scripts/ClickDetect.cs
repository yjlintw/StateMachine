using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetect : MonoBehaviour, IPointerClickHandler
{
    public bool IsRightClick
    {
        get
        {
            bool answer = _isRightClick;
            _isRightClick = false;
            return answer;
        }
        set => _isRightClick = value;
    }

    public bool IsLeftClick
    {
        get
        {
            bool answer = _isLeftClick;
            _isLeftClick = false;
            return answer;
        }
        set => _isLeftClick = value;
    }

    private bool _isLeftClick;
    private bool _isRightClick;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            IsLeftClick = true;
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            IsRightClick = true;
        }
    }
}
