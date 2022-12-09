using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinball : MonoBehaviour
{
    private AudioSource myAudioSource;

    [SerializeField] private AudioClip wallHitSound;
    // Start is called before the first frame update

    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    public void PlayWallHitSound()
    {
        myAudioSource.PlayOneShot(wallHitSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
