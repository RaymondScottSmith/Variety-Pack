using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Run_Player : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private LayerMask interactLayer;

    [SerializeField] private string enemyTag = "Enemy";

    [SerializeField] private GameObject target;

    [SerializeField] private Animator bulletAnimator;

    [SerializeField] private GameObject light;

    public float lightPosMultiplier = 10f;

    public Animator hurtAnimator;

    public bool shouldFall;

    public GameObject losePanel;

    public Slider healthSlider;

    public int maxHealth = 100;

    private int health;

    private int score;

    private int highScore;

    public TMP_Text scoreText;

    public TMP_Text highScoreText;

    public TMP_Text displayScoreText;
    
    private AudioSource audioSource;
    public AudioSource gunAudio, painAudio;

    public AudioClip fireSound, painSound, landSound;

    public float stepSpeed = 0.5f;

    public float fireRate = 0.5f;

    private bool fireWait;

    private bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        audioSource = GetComponent<AudioSource>();
        score = 0;
        health = maxHealth;
        losePanel.SetActive(false);
        shouldFall = false;
        cam = Camera.main;
        Cursor.visible = false;
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }

        highScore = PlayerPrefs.GetInt("Run_HighScore");
        
        InvokeRepeating("PlayFootstep",0,stepSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        HandleClicks();
        ControlTarget();
        
    }

    private void PlayFootstep()
    {
        audioSource.Play();
    }

    void FixedUpdate()
    {
        healthSlider.value = (float)health / (float)maxHealth;
        if (health <= 0)
        {
            shouldFall = true;
        }
    }

    private void ControlTarget()
    {
        target.transform.position = Input.mousePosition;

        var tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(tempRay, out RaycastHit hit, Mathf.Infinity))
            light.transform.LookAt(hit.point);
    }
    
    private void HandleClicks()
    {
        if (gameOver)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && !fireWait)
        {
            StartCoroutine(FireWait());
            gunAudio.PlayOneShot(fireSound);
            bulletAnimator.SetTrigger("Fire");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactLayer))
            {
                if (hit.collider.gameObject.transform.parent != null &&
                    hit.collider.gameObject.transform.parent.CompareTag("Enemy"))
                {
                    hit.collider.GetComponentInParent<Run_Ghost>().DeathAnimation();
                    
                    score++;
                    displayScoreText.text = score.ToString();
                    //hit.collider.gameObject.transform.root.transform.position = Run_Despawner.Instance.transform.position;
                }
                    
            }
        }
    }

    private IEnumerator FireWait()
    {
        fireWait = true;
        yield return new WaitForSeconds(fireRate);
        fireWait = false;
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(enemyTag) &&
            col.gameObject.GetComponentInParent<Run_Ghost>() != null &&
            col.gameObject.GetComponentInParent<Run_Ghost>().alive)
        {
            painAudio.PlayOneShot(painSound);
            hurtAnimator.SetTrigger("Hurt");
            health -= 20;
            if (health < 0)
            {
                health = 0;
            }
        }
    }

    public void CheckShouldFall()
    {
        if (!shouldFall) return;
        GetComponent<PlayableDirector>().Play();
        CancelInvoke("PlayFootstep");
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
        gameOver = true;
        audioSource.volume = 1f;
        audioSource.PlayOneShot(landSound);
        if (score > highScore)
        {
            highScore = score;
        }

        Cursor.visible = true;
        PlayerPrefs.SetInt("Run_HighScore", highScore);
        scoreText.text = "Your Score: " + score;

        highScoreText.text = "High Score: " + highScore;
        
        RunnerManager.Instance.StopMovement();
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
        losePanel.SetActive(true);
    }
}
