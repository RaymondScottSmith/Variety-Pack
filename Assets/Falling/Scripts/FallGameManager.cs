using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallGameManager : MonoBehaviour
{
    public int robotsLeft;
    public int totalRobots;
    public int maxFallingRobots = 30;
    public int fallingRobots;

    public int time = 0;

    public TMP_Text timerText, robotsText, scoreText, highScoreText;

    public bool countFalling = false;
    public float checkInterval = 3f;

    public float timeToSpawn = 10f;

    public static FallGameManager Instance;

    private AudioSource myAudio;

    [SerializeField] private GameObject endPanel;

    public bool isRunning = true;

    public Animator fadeAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        isRunning = true;
        endPanel.SetActive(false);
        myAudio = GetComponent<AudioSource>();
        robotsLeft = totalRobots;
        timerText.text = "Time: " + time.ToString();
        InvokeRepeating("IncrementTime", 1f, 1f);
        if (!countFalling)
        {
            StartCoroutine(SpawnIndividually());
        }
        else
        {
            //InvokeRepeating("CheckAndSpawn", 0, checkInterval);
            InvokeRepeating("SpawnEveryInterval",0,0.5f);
        }
    }

    public void HideCursor()
    {
        Cursor.visible = false;
    }

    private void SpawnEveryInterval()
    {
        if (totalRobots > 0)
        {
            FallSpawner.Instance.SpawnFaller();
            fallingRobots++;
            totalRobots--;
        }
        
    }

    /*
    private void CheckAndSpawn()
    {
        if (fallingRobots < maxFallingRobots && robotsLeft > 0)
        {
            FallSpawner.Instance.SpawnFaller();
            robotsLeft--;
            fallingRobots++;
        }
    }
    */
    
    void FixedUpdate()
    {
        robotsText.text = "Robots: " + (fallingRobots + totalRobots);
        if (time > 15)
            if (fallingRobots < maxFallingRobots && totalRobots > 0)
            {
                //Debug.Log("Making more robots");
                FallSpawner.Instance.SpawnFaller();
                totalRobots--;
                fallingRobots++;
            }
        
    }

    private IEnumerator SpawnFalling(float numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            FallSpawner.Instance.SpawnFaller();
            fallingRobots++;
            //currentRobots++;
            yield return new WaitForSeconds(checkInterval / numToSpawn);
        }
    }

    private IEnumerator SpawnIndividually()
    {
        for (int i = 0; i < totalRobots; i++)
        {
            FallSpawner.Instance.SpawnFaller();
            yield return new WaitForSeconds(timeToSpawn / totalRobots);
        }
        //Debug.Log("Finished Spawning");
    }

    public void RobotLanded()
    {
        //currentRobots--;
        fallingRobots--;
        //FallSpawner.Instance.SpawnFaller();
        //fallingRobots++;

    }

    private void IncrementTime()
    {
        time++;
        timerText.text = "Time: " + time.ToString();
        /*
        if (robotsLeft == 0 && fallingRobots == 0)
        {
            CancelInvoke("IncrementTime");
            CancelInvoke("CheckAndSpawn");
            StartCoroutine(EndGameSequence());
            Debug.Log("Run ends here");
        }
        */
        if (totalRobots <= 0 && fallingRobots <= 0)
        {
            CancelInvoke("IncrementTime");
            CancelInvoke("SpawnEveryInterval");
            StartCoroutine(EndGameSequence());
            //Debug.Log("Run ends here");
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        StartCoroutine(LoadMenu());
    }

    private IEnumerator LoadMenu()
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    private IEnumerator EndGameSequence()
    {
        yield return new WaitForSeconds(2f);
        Cursor.visible = true;
        isRunning = false;
        endPanel.SetActive(true);
        scoreText.text = time.ToString();
        int highScore = PlayerPrefs.GetInt("FallGame_HighScore");
        if (highScore == 0)
        {
            highScore = time;
        }
        if (time < highScore)
        {
            highScore = time;
        }

        highScoreText.text = highScore.ToString();
        PlayerPrefs.SetInt("FallGame_HighScore", highScore);
        myAudio.volume = 0.2f;
    }
}
