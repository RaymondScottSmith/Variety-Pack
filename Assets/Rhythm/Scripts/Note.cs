using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Vector3 SpawnPos, RemovePos;

    public float beatOfThisNote;

    private Conductor cond;

    [SerializeField] private float minX, maxX;
    [SerializeField] private float redOffset, yellowOffset, blueOffset, greenOffset;

    [SerializeField] private Sprite redFlower, blueFlower, greenFlower, yellowFlower;
    private bool moving;

    private SpriteRenderer myRend;

    public NoteColor noteColor;

    private Rotator myRotator;

    private bool fading;

    private Animator myAnimator;
    private AudioSource myAudio;
    private Rigidbody2D myRigidBody;

    
    public float assignedTime;

    private double timeInstantiated;
    void Awake()
    {
        moving = true;
        myRend = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAudio = GetComponent<AudioSource>();
    }
    void Start()
    {
        timeInstantiated = Conductor.GetAudioSourceTime();
        myRotator = GetComponent<Rotator>();
        myRotator.SetSpeed(Random.Range(-4,4));
        cond = Conductor.Instance;
    }

    public void Init(float beat, NoteColor nc)
    {
        float xValue = Random.Range(minX, maxX);
        
        transform.position = SpawnPos;
        beatOfThisNote = beat;
        float offset = 0;
        noteColor = nc;
        switch (nc)
        {
            case NoteColor.Red:
                myRend.sprite = redFlower;
                offset = redOffset;
                break;
            case NoteColor.Blue:
                myRend.sprite = blueFlower;
                offset = blueOffset;
                break;
            case NoteColor.Yellow:
                myRend.sprite = yellowFlower;
                offset = yellowOffset;
                break;
            case NoteColor.Green:
                myRend.sprite = greenFlower;
                offset = greenOffset;
                break;
        }

        
        SpawnPos = new Vector3(xValue + offset, SpawnPos.y, 0f);
        RemovePos = new Vector3(xValue + offset, RemovePos.y, 0f);
        moving = true;
        //Debug.Log(SpawnPos.ToString());
    }

    /*
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!moving)
            return;
        transform.position = Vector2.Lerp(SpawnPos, RemovePos,
            (cond.beatsShownInAdvance - (beatOfThisNote - cond.songPositionInBeats)) / cond.beatsShownInAdvance);

        if (transform.position == RemovePos && !fading)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetTrigger("Shrink");
            GetComponent<AudioSource>().Play();
            fading = true;
            StartCoroutine(Conductor.Instance.PauseMusic());
            RhythmManager.Instance.MissedNote();
        }
    }
    
    */
    
    void Update()
    {
        double timeSinceInstantiated = Conductor.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (Conductor.Instance.noteTime * 2));

        if (moving)
        {
            if (t > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(Vector3.up * Conductor.Instance.noteSpawnY, Vector3.up * Conductor.Instance.noteDespawnY, t); 
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        
    }

    public void DestroyNote()
    {
        Destroy(gameObject);
    }

    public void SpindAndDie()
    {
        //Debug.Log("Should be spinning");
        moving = false;
        myRigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
        myAnimator.SetTrigger("Shrink");
    }
}
