using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollision : MonoBehaviour
{

    [SerializeField] private string targetTag = "Player";
    
    publi
    private void OnCollisionExit2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag(targetTag))
        {
            return;
        }
        
    }
}
