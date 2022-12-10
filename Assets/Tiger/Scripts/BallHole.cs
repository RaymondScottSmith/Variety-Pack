using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHole : MonoBehaviour
{

    public static BallHole Instance;

    [SerializeField] private GameObject basicBallPrefab;

    [SerializeField] private GameObject neonTigerPrefab;

    [SerializeField] private float launchSpeed = 30f;

    [SerializeField] private AudioClip dropBall, shootBall;

    private AudioSource myAudioSource;
    

    private bool isLaunching;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    
    [ContextMenu("LaunchBall")]
    public void LaunchBall()
    {
        myAudioSource.PlayOneShot(shootBall);
        isLaunching = true;
        GameObject newBall = Instantiate(basicBallPrefab, transform.position, basicBallPrefab.transform.rotation);

        int directionDecider = Random.Range(0, 2);
        Vector2 direction = new Vector2();
        if (directionDecider == 0)
        {
            Debug.Log("Launching Left");
            direction = Vector2.left;
        }
        else
        {
            Debug.Log("Launching Right");
            direction = Vector2.right;
        }
        newBall.GetComponent<Rigidbody2D>().AddForce(direction * launchSpeed, ForceMode2D.Impulse);
    }

    public void LaunchNeonTiger()
    {
        isLaunching = true;
        GameObject newBall = Instantiate(neonTigerPrefab, transform.position, neonTigerPrefab.transform.rotation);

        int directionDecider = Random.Range(0, 2);
        Vector2 direction = new Vector2();
        if (directionDecider == 0)
        {
            Debug.Log("Launching Left");
            direction = Vector2.left;
        }
        else
        {
            Debug.Log("Launching Right");
            direction = Vector2.right;
        }
        newBall.GetComponent<Rigidbody2D>().AddForce(direction * launchSpeed, ForceMode2D.Impulse);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball") && !isLaunching)
        {
            myAudioSource.PlayOneShot(dropBall);
            PinballManager.Instance.BallInHole(col.gameObject);
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball") && isLaunching)
        {
            isLaunching = false;
        }
    }
}
