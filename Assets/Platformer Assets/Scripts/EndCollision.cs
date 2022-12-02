using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndCollision : MonoBehaviour
{

    [SerializeField] private string targetTag = "Player";
    
    public UnityEvent OnEndCollisionEvent;
    private void OnCollisionExit2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag(targetTag))
        {
            return;
        }
        OnEndCollisionEvent?.Invoke();
    }
}
