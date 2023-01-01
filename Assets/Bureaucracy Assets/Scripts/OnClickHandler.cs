using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnClickHandler : MonoBehaviour, IPointerClickHandler
{
    
    public UnityEvent OnClickEvent;

    void OnMouseDown()
    {
        OnClickEvent?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke();
    }
}
