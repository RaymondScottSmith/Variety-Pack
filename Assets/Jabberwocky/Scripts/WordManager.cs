using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordManager : MonoBehaviour
{
    public List<JA_Word> words;

    private bool hasActiveWord;
    public JA_Word activeWord;

    public JA_WordSpawner wordSpawner;

    public GameObject hero;
    public Animator heroAnimator;

    public static WordManager Instance;

    private int score;

    public GameObject losePanel, winPanel;

    private AudioSource myAudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    private void Start()
    {
        score = 0;
        myAudioSource = GetComponent<AudioSource>();
        wordSpawner = GetComponent<JA_WordSpawner>();
    }

    public void AddScore(int add)
    {
        score += add;
    }

    public void AddWord()
    {
        JA_Word word = new JA_Word(JA_WordGenerator.GetRandomWord(), wordSpawner.SpawnWord());
        //Debug.Log(word.word);
        words.Add(word);
    }

    public void TypeLetter(char letter)
    {
        if (hasActiveWord)
        {
            //Check if letter was next
                //Remove it from the word
                if (activeWord.GetNextLetter() == letter)
                {
                    activeWord.TypeLetter();
                }
        }
        else
        {
            foreach (JA_Word word in words)
            {
                if (word.GetNextLetter() == letter)
                {
                    activeWord = word;
                    hasActiveWord = true;
                    word.TypeLetter();
                    break;
                }
            }
        }

        if (hasActiveWord && activeWord.WordComplete())
        {
            hasActiveWord = false;
            words.Remove(activeWord);
        }
    }

    public void RemoveWord(JA_Word word)
    {
        if (hasActiveWord && activeWord == word)
        {
            hasActiveWord = false;
            
        }
        words.Remove(word);
    }

    public void SlashCreature(Vector2 pos, Animator animator)
    {
        myAudioSource.Play();
        hero.transform.position = new Vector2(hero.transform.position.x, pos.y);
        heroAnimator.SetTrigger("Attack");
    }

    public void LoseGame()
    {
        GetComponent<JA_WordInput>().isOver = true;
        GetComponent<JA_WordTimer>().isOver = true;
        losePanel.SetActive(true);
    }

    public void WinGame()
    {
        GetComponent<JA_WordInput>().isOver = true;
        GetComponent<JA_WordTimer>().isOver = true;
        winPanel.SetActive(true);
    }

    public void SpawnJabber()
    {
        Debug.Log("Spawn Jabberwock here");
        WordDisplay test = wordSpawner.SpawnJabber();
        JA_Word jabberWord = new JA_Word(test.GetComponent<Jabberwock>().poem, test);
        words.Add(jabberWord);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
