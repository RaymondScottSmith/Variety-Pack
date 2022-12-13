using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PinballManager : MonoBehaviour
{

    [Header("Prefabs")] [SerializeField] private GameObject basicBallPrefab;
    
    [Header("UI")]
    [SerializeField] private TMP_Text livesText;

    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private TMP_Text highScoreText;

    [SerializeField] private GameObject losePanel;

    [SerializeField] private GameObject neonAnnounce;

    [SerializeField] private GameObject circusAnnounce, caveAnnounce;

    [SerializeField] private TMP_Text endScore;
    [SerializeField] private TMP_Text endHigh;

    [SerializeField] private TMP_Text caveInstructions;

    [Header("Other")] [SerializeField] private Transform ballSpawnLocation;
    [SerializeField] private int startingLives = 3;
    [SerializeField] private TigerCage tigerCage;

    [SerializeField] private List<DiamondLight> lights;

    [SerializeField] private GameObject circus;

    [SerializeField] private float circusModeDuration = 30f;

    [SerializeField] private Rotator circleRotator;

    [SerializeField] private GameObject holeLock;

    [SerializeField] private AudioClip normalMusic;
    [SerializeField] private AudioClip circusMusic;
    [SerializeField] private AudioClip neonMusic, loseMusic;


    public static PinballManager Instance;

    private int lives;

    private int score;

    private int highScore;

    private AudioSource musicSource;

    private bool gameOver;
    

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        score = 0;
        SpawnBall();
        lives = startingLives;
        livesText.text = "Lives: " + lives;
        highScore = PlayerPrefs.GetInt("PinballHScore");
        highScoreText.text = highScore.ToString();
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = normalMusic;
        musicSource.Play();
        losePanel.SetActive(false);

        caveInstructions.text = "Send 4 Tigers To The Cave";
    }

    public void ChangeScore(int addedValue)
    {
        score += addedValue;
    }

    public void DestroyBall(GameObject ball)
    {
        if (ball.CompareTag("Ball"))
        {
            Destroy(ball);


            StartCoroutine(KillReplaceBall());

        }
    }

    private IEnumerator KillReplaceBall()
    {
        yield return new WaitForFixedUpdate();
        Pinball[] ballsInScene = FindObjectsOfType<Pinball>();
        if (ballsInScene.Length <= 0)
        {
            if (UpdateLives(-1))
                SpawnBall();
            else
            {
                  LoseGame();  
            }
        }
    }

    [ContextMenu("Test Light Activation")]
    public void TestBallInHole()
    {
        foreach (DiamondLight light in lights)
        {
            if (!light.isLit)
            {
                light.ActivateLight();
                break;
            }
        }

        int lightsNeeded = 4;
        foreach (DiamondLight light in lights)
        {
            if (light.isLit)
            {
                lightsNeeded--;
            }
        }

        caveInstructions.text = "Send " + lightsNeeded + " Tigers To The Cave";
        
        foreach (DiamondLight light in lights)
        {
            if (!light.isLit)
            {
                return;
            }
        }

        CircusMode();
    }
    public void BallInHole(GameObject ball)
    {
        Destroy(ball);

        StartCoroutine(ReplaceBall());
        

        foreach (DiamondLight light in lights)
        {
            if (!light.isLit)
            {
                light.ActivateLight();
                break;
            }
        }
        
        int lightsNeeded = 4;
        foreach (DiamondLight light in lights)
        {
            if (light.isLit)
            {
                lightsNeeded--;
            }
        }

        caveInstructions.text = "Send " + lightsNeeded + " Tigers To The Cave";
        
        foreach (DiamondLight light in lights)
        {
            if (!light.isLit)
            {
                return;
            }
        }

        CircusMode();
    }

    private IEnumerator ReplaceBall()
    {
        yield return new WaitForFixedUpdate();
        Pinball[] ballsInScene = FindObjectsOfType<Pinball>();
        if (ballsInScene.Length <= 0)
        {
            SpawnBall();
        }
    }

    public void CircusMode()
    {
        //Debug.Log("Circus Mode is on!");
        StartCoroutine(RunCircusMode());
        
    }

    private IEnumerator RunCircusMode()
    {
        caveAnnounce.SetActive(false);
        circusAnnounce.SetActive(true);
        musicSource.Stop();
        musicSource.loop = true;
        musicSource.clip = circusMusic;
        musicSource.Play();
        holeLock.SetActive(true);
        circus.SetActive(true);
        circleRotator.rotating = true;
        yield return new WaitForSeconds(circusModeDuration);

        musicSource.Stop();
        circus.SetActive(false);
        circleRotator.rotating = false;
        holeLock.SetActive(false);
        circusAnnounce.SetActive(false);
        caveAnnounce.SetActive(true);
        foreach (DiamondLight light in lights)
        {
            light.DeActivateLight();
        }
        caveInstructions.text = "Send 4 Tigers To The Cave";

    }

    public void CircusDestroyed()
    {
        circusAnnounce.SetActive(false);
        neonAnnounce.SetActive(true);
        StopCoroutine(RunCircusMode());
        musicSource.Stop();
        circus.SetActive(false);
        circleRotator.rotating = false;
        holeLock.SetActive(false);
        foreach (DiamondLight light in lights)
        {
            light.DeActivateLight();
        }

        StartCoroutine(NeonTigerActivate());
    }


    private IEnumerator NeonTigerActivate()
    {
        musicSource.Stop();
        musicSource.loop = false;
        musicSource.clip = neonMusic;
        musicSource.Play();
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 4; i++)
        {
            BallHole.Instance.LaunchNeonTiger();
            yield return new WaitForSeconds(1f);
        }
        
    }
    public bool UpdateLives(int change)
    {
        lives += change;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            return false;
        }

        return true;
    }

    private void FixedUpdate()
    {
        scoreText.text = score.ToString();

        if (!musicSource.isPlaying && !gameOver)
        {
            if (neonAnnounce.activeSelf)
            {
                neonAnnounce.SetActive(false);
            }
            caveAnnounce.SetActive(true);
            musicSource.clip = normalMusic;
            musicSource.loop = true;
            musicSource.Play();
            caveInstructions.text = "Send 4 Tigers To The Cave";
        }
    }

    private void LoseGame()
    {
        gameOver = true;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("PinballHScore", highScore);
        }
            
        musicSource.Stop();
        musicSource.PlayOneShot(loseMusic);
        losePanel.gameObject.SetActive(true);
        endScore.text = "Your Score: " + score.ToString();
        endHigh.text = "High Score: " + highScore;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SpawnBall()
    {
        Instantiate(basicBallPrefab, ballSpawnLocation.position, basicBallPrefab.transform.rotation);
        tigerCage.SetBlocker(false);
    }
    
}
