using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JA_Word
{
    public string word;
    private int letterIndex;
    public JA_Word(string myWord)
    {
        word = myWord;
        letterIndex = 0;
    }

    public char GetNextLetter()
    {
        return word[letterIndex];
    }

    public void TypeLetter()
    {
        letterIndex++;
        //Remove letter on screen
    }

    public bool WordComplete()
    {
        bool finished = letterIndex >= word.Length;
        if (finished)
        {
            //Remove word on screen
        }

        return finished;
    }
}
