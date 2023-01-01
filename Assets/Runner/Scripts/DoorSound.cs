using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    private AudioSource myAudio;

    private bool playingSound;

    public float resetTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {/*
        if (!playingSound)
        {
            playingSound = true;
            StartCoroutine(PlaySoundCoroutine());
        }
        */
        if (!myAudio.isPlaying)
        {
            myAudio.Play();
        }
    }

    private IEnumerator PlaySoundCoroutine()
    {
        myAudio.Play();
        yield return new WaitForSeconds(resetTime);
        playingSound = false;
    }
    
}
