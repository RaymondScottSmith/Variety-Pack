using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectCollider : MonoBehaviour
{
    private AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Note"))
        {
            GetComponentInParent<Paddle>().notWaiting = false;
            RhythmManager.Instance.ScorePerfect(col.GetComponent<Note>().noteColor);
            col.GetComponent<Note>().SpindAndDie();
            myAudioSource.Play();
        }
    }
}
