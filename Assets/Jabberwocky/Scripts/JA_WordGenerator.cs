using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JA_WordGenerator : MonoBehaviour
{
    public static JA_WordGenerator Instance;

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
    private static string[] wordList = { };
    private void Start()
    {
        TextAsset wordFile = Resources.Load<TextAsset>("JA_WordList");

        wordList = wordFile.text.Split(new char[] { ',' });
    }
    
    public static string GetRandomWord()
    {
        int randomIndex = Random.Range(0, wordList.Length);
        string randomWord = wordList[randomIndex];

        return randomWord;
    }
}
