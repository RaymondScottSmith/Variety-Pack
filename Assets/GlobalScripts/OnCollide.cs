using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollide : MonoBehaviour
{
    [SerializeField] private string collideTag;

    public UnityEvent OnCollideEvent;
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(collideTag))
        {
            OnCollideEvent?.Invoke();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(collideTag))
        {
            OnCollideEvent?.Invoke();
        }
    }
}
