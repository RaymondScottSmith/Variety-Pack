using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_SoundManager : MonoBehaviour
{
    private AudioSource myAudio;

    [SerializeField]
    private List<AudioClip> soundList;
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void PlayClip(int soundNo, float pitch = 1f, float volume = 0.5f)
    {
        StopAudio();
        myAudio.pitch = pitch;
        myAudio.volume = volume;
        myAudio.PlayOneShot(soundList[soundNo]);
    }

    public void PlayRepeating(int soundNo, float pitch = 1f, float volume = 0.5f)
    {
        StopAudio();
        myAudio.pitch = pitch;
        myAudio.volume = volume;
        myAudio.clip = soundList[soundNo];
        myAudio.Play();
    }
    public void StopAudio()
    {
        myAudio.Stop();
    }
}
