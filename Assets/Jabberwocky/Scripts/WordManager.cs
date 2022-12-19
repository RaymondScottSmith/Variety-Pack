using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public List<JA_Word> words;

    private bool hasActiveWord;
    private JA_Word activeWord;

    public JA_WordSpawner wordSpawner;
    

    private void Start()
    {
        wordSpawner = GetComponent<JA_WordSpawner>();
    }

    public void AddWord()
    {
        JA_Word word = new JA_Word(JA_WordGenerator.GetRandomWord(), wordSpawner.SpawnWord());
        Debug.Log(word.word);
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
    
}
