using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JA_WordInput : MonoBehaviour
{
    public WordManager wordManager;

    public bool isOver;

    void Start()
    {
        wordManager = FindObjectOfType<WordManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOver)
        {
            return;
        }
        foreach (char letter in Input.inputString)
        {
            wordManager.TypeLetter(letter);
        }
    }
}
