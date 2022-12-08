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

    [SerializeField] private float speed = 30f;

    [SerializeField]
    private AudioClip launchSound;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
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
        myAudioSource.PlayOneShot(launchSound);
        yield return new WaitForSeconds(0.5f);
        ballBody.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
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
