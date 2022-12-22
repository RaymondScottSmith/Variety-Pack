using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingThing : MonoBehaviour
{
    private Rigidbody2D myRigid;
    private bool parachute = true;
    [SerializeField] private float fallSpeed = 1f, walkSpeed = 1f;
    [SerializeField]
    private Animator paraAnimator, robotAnimator;

    [SerializeField] private SpriteRenderer paraSprite, robotSprite;

    [SerializeField] private AudioSource myAudio;
    [SerializeField] private AudioClip hardLand,softLand;
    private bool isWalking;
    private Vector2 direction;
    private bool landed;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myRigid = GetComponent<Rigidbody2D>();
        if (transform.position.x > 0)
        {
            //paraSprite.flipX = true;
            //robotSprite.flipX = true;
        }
    }
    public void StartFreefalling()
    {
        myRigid.gravityScale = 1f;
        parachute = false;
    }

    private void FixedUpdate()
    {
        if (parachute)
        {
            transform.Translate(Vector3.down * fallSpeed);
        }

        if (isWalking)
        {
            transform.Translate(direction * walkSpeed);
            if (Mathf.Abs(transform.position.x) - 3.5f >= FallSpawner.Instance.right.x)
            {
                Destroy(gameObject);
            }
        }
    }

    public void RobotFreefalls()
    {
        robotAnimator.SetTrigger("Fall");
    }

    public void WalkToExit()
    {
        if (transform.position.x > 0)
        {
            direction = Vector2.right;
            
        }
        else
        {
            direction = Vector2.left;
            robotSprite.flipX = true;
        }

        isWalking = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (!landed)
        {
            landed = true;
            if (col.gameObject.CompareTag("Wall"))
            {
                FallGameManager.Instance.RobotLanded();
                if (paraAnimator.gameObject.activeSelf)
                {
                    paraAnimator.SetTrigger("Land");
                    robotAnimator.SetTrigger("Soft");
                    myAudio.PlayOneShot(softLand);
                }
                else
                {
                    myAudio.volume = 0.25f;
                    myAudio.PlayOneShot(hardLand);
                    robotAnimator.SetTrigger("Land");
                }
                Invoke("WalkToExit",1.5f);
            }
        }
        
            
    }
}
