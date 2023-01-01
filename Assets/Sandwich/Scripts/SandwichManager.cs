using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SandwichManager : MonoBehaviour
{
    public static SandwichManager Instance;

    public bool checkingMenu;

    public TMP_Text orderText, moneyText, timeText, scoreText, highText;
    public Sa_ServeArea servingArea;

    public GameObject endPanel;

    [SerializeField]
    private List<Sandwich> availableSandwiches;

    private Sandwich currentSandwich;

    private int money = 0;
    private int highScore = 0;
    private AudioSource myAudio;

    public Animator signAnimator;

    [SerializeField] private int lengthOfShift = 90;

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
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        moneyText.text = "Income: $" + money;
        highScore = PlayerPrefs.GetInt("SandwichHigh");
        endPanel.SetActive(false);
        SetNewSandwich();
        InvokeRepeating("TickTime",1,1);
    }

    private void TickTime()
    {
        lengthOfShift--;
        if (lengthOfShift < 0)
            lengthOfShift = 0;
    }

    public Sandwich RandomSandwich()
    {
        return availableSandwiches[Random.Range(0, availableSandwiches.Count)];
    }

    public void SetNewSandwich()
    {
        myAudio.Play();
        signAnimator.SetTrigger("Flash");
        currentSandwich = RandomSandwich();
        orderText.text = "ORDER: " + currentSandwich.name;
        servingArea.SetNewSandwich(currentSandwich);
    }

    public void SandwichCompleted()
    {
        money += currentSandwich.cost;
        moneyText.text = "Income: $" + money;
        SetNewSandwich();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lengthOfShift <= 0 && !endPanel.activeSelf)
        {
            timeText.text = "Shift Ends: " + lengthOfShift;
            EndGame();
        }
        else if (!endPanel.activeSelf)
        {
            timeText.text = "Shift Ends: " + lengthOfShift;
        }
        
        
    }

    private void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endPanel.SetActive(true);
        if (money > highScore)
        {
            highScore = money;
        }

        scoreText.text = "Your Score: $" + money;
        highText.text = "High Score: $" + highScore;
        PlayerPrefs.SetInt("SandwichHigh", highScore);
        
        Time.timeScale = 0f;
        
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(MenuLoad());
    }

    private IEnumerator MenuLoad()
    {
        
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
