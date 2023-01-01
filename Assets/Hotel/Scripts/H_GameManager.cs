using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class H_GameManager : MonoBehaviour
{
    public static H_GameManager Instance;
    public int zombiesInScene;

    [SerializeField] private GameObject winPanel, losePanel, pausePanel;

    [SerializeField] private TMP_Text instructText;
    [SerializeField] private TMP_Text z_count;

    public Animator fadeAnimator;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        zombiesInScene = FindObjectsOfType<H_Zombie>().Length;
        Debug.Log(zombiesInScene);
    }

    public void UpdateInstructions(string message)
    {
        instructText.text = message;
    }

    public void DeadZombie()
    {
        zombiesInScene--;
        z_count.text = "Zombies Remaining: " + zombiesInScene;
        if (zombiesInScene <= 0)
        {
            UpdateInstructions("Now Use The Stairs");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (winPanel.activeSelf || losePanel.activeSelf)
            {
                return;
            }
            else if (pausePanel.activeSelf)
            {
                Time.timeScale = 1f;
                pausePanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
            }
        }
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        UpdateInstructions("");
        winPanel.SetActive(true);
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        UpdateInstructions("");
        losePanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(FadeTransition());
    }

    private IEnumerator FadeTransition()
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
