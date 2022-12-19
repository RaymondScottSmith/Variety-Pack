using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JA_WordGenerator : MonoBehaviour
{
    private void Start()
    {
        TextAsset wordFile = Resources.Load<TextAsset>("JA_WordList");

        string[] data = wordFile.text.Split(new char[] { ',' });
        foreach (string word in data)
        {
            Debug.Log(word);
        }
    }
    private static string[] wordList = { };
    public static string GetRandomWord()
    {
        return null;
    }
}
