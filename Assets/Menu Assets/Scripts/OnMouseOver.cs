using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnMouseOver : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent OnHighlight;
    private void OnMouseEnter()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse Over");
        OnHighlight?.Invoke();
    }
}
