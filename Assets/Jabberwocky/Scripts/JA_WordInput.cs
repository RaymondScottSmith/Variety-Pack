using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JA_WordInput : MonoBehaviour
{
    public WordManager wordManager;

    void Start()
    {
        wordManager = FindObjectOfType<WordManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (char letter in Input.inputString)
        {
            wordManager.TypeLetter(letter);
        }
    }
}
