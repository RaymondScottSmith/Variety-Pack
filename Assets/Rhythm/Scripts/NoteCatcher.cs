using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCatcher : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Note"))
        {
            Debug.Log("Missed a Note");
            RhythmManager.Instance.MissedNote();
            col.GetComponent<Note>().DestroyNote();
        }
    }
}
