using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OnClickHandler : MonoBehaviour
{
    
    public UnityEvent OnClickEvent;

    void OnMouseDown()
    {
        OnClickEvent?.Invoke();
    }
}
