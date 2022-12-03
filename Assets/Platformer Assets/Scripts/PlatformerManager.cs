using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformerManager : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp;

    [SerializeField] private int time = 3;

    [SerializeField] private TMP_Text inGameTimer;

    [SerializeField] private TMP_Text highScoreText;

    [SerializeField] private GameObject losePanel;

    private PlayerJump player;

    public static PlatformerManager Instance;

    private int timer;

    private int highScore;
    
    
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("PlatformerScore");
        highScoreText.text = "High Score: " + highScore; 
        losePanel.SetActive(false);
        timer = 0;
        if (PlatformerManager.Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        player = FindObjectOfType<PlayerJump>();
        StartCoroutine(StartingCountdown());
    }

    public void StartTimer()
    {
        InvokeRepeating("Timer",1f,1f);
    }

    private void Timer()
    {
        timer++;
        inGameTimer.text = "Score: " + timer.ToString();
    }

    public void LoseSetup()
    {
        CancelInvoke("Timer");
        if (timer > highScore)
        {
            highScore = timer;
        }
        highScoreText.text = "High Score: " + highScore; 
        PlayerPrefs.SetInt("PlatformerScore", highScore);
        losePanel.gameObject.SetActive(true);
    }

    private IEnumerator StartingCountdown()
    {
        while (time > 0)
        {
            tmp.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
        }

        tmp.text = "Jump!";
        yield return new WaitForSeconds(1);
        tmp.text = "";
        player.lost = false;
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        Debug.Log("Add Menu Scene Later");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
