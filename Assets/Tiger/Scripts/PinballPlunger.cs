using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PinballPlunger : MonoBehaviour, IPointerClickHandler
{
    private bool touchingBall;

    private Rigidbody2D ballBody;

    [SerializeField] private float speed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touchingBall && ballBody != null && Input.GetKeyUp(KeyCode.Space))
        {
            ballBody.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        }
    }

    void OnMouseDown()
    {
        Debug.Log("On Mouse Down");
        if (touchingBall && ballBody != null)
        {
            ballBody.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Detecting Collision");
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
