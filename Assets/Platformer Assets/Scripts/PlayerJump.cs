using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [SerializeField] private float minJumpSpeed = 1f;

    [SerializeField] private string jumpTag = "Jumpable";

    [SerializeField] private float maxJumpTime = 1f;

    [SerializeField] private float maxJumpSpeed = 15f;

    [SerializeField] private Sprite restingSprite, jumpingSprite;
    
    [SerializeField] private AudioClip jumpSound;
    
    [SerializeField] private AudioClip deathSound;

    private float jumpSpeed;

    private bool isGrounded;

    private Rigidbody2D rb2D;

    private Vector2 contactNormal;

    private TrajectoryPredictor trajProj;

    private bool isChargingJump;

    private bool firstJump;

    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;

    public bool lost;

    // Start is called before the first frame update
    void Start()
    {
        lost = true;
        isChargingJump = false;
        trajProj = gameObject.AddComponent<TrajectoryPredictor>();
        trajProj.predictionType = TrajectoryPredictor.predictionMode.Prediction2D;
        trajProj.drawDebugOnPrediction = true;
        trajProj.accuracy = 0.99f;
        trajProj.lineWidth = 0.025f;
        trajProj.iterationLimit = 300;
        trajProj.checkForCollision = true;
        trajProj.raycastMask = trajProj.raycastMask+8;
        //trajProj.checkForCollision = false;
        rb2D = GetComponent<Rigidbody2D>();
        firstJump = true;
        //isGrounded = true;
        jumpSpeed = minJumpSpeed;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (lost)
            return;
        spriteRenderer.sprite = isGrounded ? restingSprite : jumpingSprite;
        
        
        if (Input.GetKey(KeyCode.Space) && isGrounded && contactNormal != Vector2.zero && !isChargingJump)
        {
            jumpSpeed = minJumpSpeed;
            isChargingJump = true;
            StartCoroutine(BuildJumpSpeed());
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && contactNormal != Vector2.zero)
        {
            jumpSpeed = minJumpSpeed;
            isChargingJump = true;
            //Debug.Log("Should Be Jumping");
            StartCoroutine(BuildJumpSpeed());
            
        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded && contactNormal != Vector2.zero)
        {
            if (firstJump)
            {
                PlatformerManager.Instance.StartTimer();
            }
            isChargingJump = false;
            isGrounded = false;
            if (jumpSpeed > maxJumpSpeed)
                jumpSpeed = maxJumpSpeed;
            
            audioSource.PlayOneShot(jumpSound);
            firstJump = false;
            rb2D.AddForce(contactNormal * (jumpSpeed), ForceMode2D.Impulse);
            contactNormal = Vector2.zero;
            jumpSpeed = minJumpSpeed;
        }
        
        if ((isGrounded || firstJump) && isChargingJump)
        {
            //set line duration to delta time so that it only lasts the length of a frame
            trajProj.debugLineDuration = Time.unscaledDeltaTime;
            //tell the predictor to predict a 2d line. this will also cause it to draw a prediction line
            //because drawDebugOnPredict is set to true
            trajProj.Predict2D(transform.position, contactNormal * jumpSpeed, Physics2D.gravity);
        }
    }

    private IEnumerator BuildJumpSpeed()
    {
        while (jumpSpeed < maxJumpSpeed && isGrounded)
        {
            jumpSpeed++;
            yield return new WaitForSeconds(maxJumpTime / (maxJumpSpeed - minJumpSpeed));
        }

        if (jumpSpeed > maxJumpSpeed)
        {
            jumpSpeed = maxJumpSpeed;
        }
    }

    public IEnumerator LoseGame()
    {
        lost = false;
        yield return new WaitForSeconds(0.05f);
        audioSource.PlayOneShot(deathSound);
        Debug.Log("Ending Sequence Here");
        yield return new WaitForSeconds(1f);
        PlatformerManager.Instance.LoseSetup();
    }

    void FixedUpdate()
    {
        
        
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        //Debug.Log("collider: " + col.contacts[0].point.y);
        //Debug.Log("player: " + transform.position.y);
        if (!col.gameObject.CompareTag(jumpTag))
        {
            return;
        }
        
        else if (col.contacts[0].point.y < transform.position.y)
        {
            contactNormal = col.contacts[0].normal;
            isGrounded = true;
        }

    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag(jumpTag))
        {
            return;
        }
        StopCoroutine(BuildJumpSpeed());
        jumpSpeed = minJumpSpeed;
        isChargingJump = false;
        if(!firstJump)
            isGrounded = false;
        contactNormal = Vector2.zero;
    }
}
