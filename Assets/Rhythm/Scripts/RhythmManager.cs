using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance;

    [SerializeField] private GameObject redBack, blueBack, greenBack, yellowBack;
    [SerializeField] private int startingMultiplier = 1;

    [SerializeField] private TMP_Text scoreText, multText, hScoreText, finalScoreText, japScoreText;

    [SerializeField] private GameObject endPanel, startPanel;

    private int scoreMultiplier, score, scoreCount, highScore;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        scoreCount = 0;
        scoreMultiplier = startingMultiplier;
        highScore = PlayerPrefs.GetInt("RhythmScore_Jap");
        japScoreText.text = "High Score: " + highScore;
        endPanel.SetActive(false);
        startPanel.SetActive(true);

    }

    public void StartSong(int songNumber)
    {
        if (songNumber == 1)
        {
            startPanel.SetActive(false);
            Conductor.Instance.StartSong(songNumber);
        }
        
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowEndPanel()
    {
        endPanel.SetActive(true);
        finalScoreText.text = score.ToString();
        if (score > highScore)
        {
            highScore = score;
            endPanel.GetComponentInChildren<Animator>().SetBool("Flashing", true);
            PlayerPrefs.SetInt("RhythmScore_Jap", highScore);
        }
        hScoreText.text = highScore.ToString();
    }

    private void IncreaseScore(int points)
    {
        scoreCount++;
        if (scoreCount == 4)
        {
            scoreCount = 0;
            scoreMultiplier++;
            if (scoreMultiplier > 8)
                scoreMultiplier = 8;
        }

        score += points * scoreMultiplier;
    }

    public void MissedNote()
    {
        scoreCount = 0;
        scoreMultiplier = 1;
    }

    void FixedUpdate()
    {
        scoreText.text = "SCORE: " + score;
        multText.text = "Multiplier: X" + scoreMultiplier;
    }

    public void ScoreNormal(NoteColor color)
    {
        switch (color)
        {
            case NoteColor.Red:
                //Debug.Log("Got a red note");
                break;
            case NoteColor.Blue:
                //Debug.Log("Got a blue note");
                break;
            case NoteColor.Green:
                //Debug.Log("Got a green note");
                break;
            case NoteColor.Yellow:
                //Debug.Log("Got a yellow note");
                break;
        }
        IncreaseScore(10);
    }

    public void ScorePerfect(NoteColor color)
    {
        GameObject colorBack = null;
        switch (color)
        {
            case NoteColor.Red:
                colorBack = redBack;
                //Debug.Log("Got a perfect red note");
                break;
            case NoteColor.Blue:
                colorBack = blueBack;
                //Debug.Log("Got a perfect blue note");
                break;
            case NoteColor.Green:
                colorBack = greenBack;
                //Debug.Log("Got a perfect green note");
                break;
            case NoteColor.Yellow:
                colorBack = yellowBack;
               // Debug.Log("Got a perfect yellow note");
                break;
        }
        if (colorBack != null)
            colorBack.GetComponent<Animator>().SetTrigger("Flash");
        
        IncreaseScore(50);
    }
}
