using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OnEnter : MonoBehaviour
{
    [SerializeField] private string enterTag;

    public UnityEvent OnEnterEvent, OnExitEvent;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(enterTag))
        {
            OnEnterEvent?.Invoke();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(enterTag))
        {
            OnEnterEvent?.Invoke();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag(enterTag))
        {
            OnExitEvent?.Invoke();
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(enterTag))
        {
            OnExitEvent?.Invoke();
        }
    }
}
