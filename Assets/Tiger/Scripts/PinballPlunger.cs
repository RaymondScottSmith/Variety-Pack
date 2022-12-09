using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PinballPlunger : MonoBehaviour, IPointerClickHandler
{
    private bool touchingBall;

    private Rigidbody2D ballBody;

    private AudioSource myAudioSource;

    private Animator myAnimator;

    [SerializeField] private float speed = 30f;

    [SerializeField] private float speedVariance = 5f;

    [SerializeField]
    private AudioClip launchSound;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponentInChildren<AudioSource>();
        myAnimator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (touchingBall && ballBody != null && Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(LaunchBall());
        }
    }

    void OnMouseDown()
    {
        Debug.Log("On Mouse Down");
        if (touchingBall && ballBody != null)
        {
            StartCoroutine(LaunchBall());
        }
    }

    private IEnumerator LaunchBall()
    {
        myAnimator.SetTrigger("Pull");
        myAudioSource.PlayOneShot(launchSound);
        yield return new WaitForSeconds(0.5f);
        
        yield return new WaitForSeconds(0.45f);

        float speedOffset = Random.Range(-speedVariance, speedVariance);
        ballBody.AddForce(Vector2.up * (speed + speedVariance), ForceMode2D.Impulse);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            touchingBall = true;
            ballBody = col.gameObject.GetComponent<Rigidbody2D>();
        }
            
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            touchingBall = false;
            ballBody = null;
        }
            
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Detect Click");
        if (touchingBall && ballBody != null)
        {
            ballBody.AddForce(Vector2.up * speed);
        }
    }
}
