using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JA_Word
{
    public string word;
    private int letterIndex;
    private WordDisplay display;
    public JA_Word(string myWord, WordDisplay newDisplay)
    {
        display = newDisplay;
        word = myWord;
        letterIndex = 0;
        display.SetWord(word);
    }

    public char GetNextLetter()
    {
        return word[letterIndex];
    }

    public void TypeLetter()
    {
        letterIndex++;
        //Remove letter on screen
        display.RemoveLetter();
    }

    public bool WordComplete()
    {
        bool finished = letterIndex >= word.Length;
        if (finished)
        {
            //Remove word on screen
            display.RemoveWord();
        }

        return finished;
    }
    
}
