using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class H_Interactive : MonoBehaviour
{
    public Transform interactPos;
    public GameObject mover;
    public bool moverInPosition;
    public bool selected;
    public float positionWiggle;
    
    public void MoveToInteract(GameObject newMover)
    {
        mover = newMover;
        mover.GetComponent<NavMeshAgent>().destination = interactPos.position;
        selected = true;
    }

    public void CancelSelect()
    {
        selected = false;
        mover = null;
    }

    void OnTriggerEnter(Collider col)
    {
        if (mover != null && col.gameObject.CompareTag(mover.tag))
        {
           Interact();
        }
    }

    public abstract void Interact();
}
