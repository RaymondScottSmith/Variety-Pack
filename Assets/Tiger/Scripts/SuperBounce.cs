using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBounce : MonoBehaviour
{
    [SerializeField] private float ricochetForce = 10f;

    private AudioSource myAudioSource;

    private Animator myAnimator;

    void Start()
    {
        myAudioSource = GetComponentInParent<AudioSource>();
        myAnimator = GetComponentInParent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            myAudioSource.Play();
            myAnimator.SetTrigger("Shake");
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector2 angle = col.GetContact(0).normal;
            rb.AddForce(angle * ricochetForce, ForceMode2D.Impulse);
        }
    }
}
