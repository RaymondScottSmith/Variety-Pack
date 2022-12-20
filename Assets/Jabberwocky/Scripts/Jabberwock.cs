using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class Jabberwock : MonoBehaviour
{
    [TextArea]
    public string poem;
    private int letterIndex = 0;
    public TMP_Text text;

    public JA_Word word;

    private void Start()
    {
        text.text = poem;

        word = new JA_Word(poem, GetComponent<WordDisplay>());
    }
    
    public char GetNextLetter()
    {
        return poem[letterIndex];
    }

    public void TypeLetter()
    {
        letterIndex++;
        //Remove letter on screen
        RemoveLetter();
    }
    
    public void RemoveLetter()
    {
        text.text = text.text.Remove(0, 1);
        text.color = Color.red;
        
    }

    public bool WordComplete()
    {
        bool finished = letterIndex >= poem.Length;
        if (finished)
        {
            //Remove word on screen
            Debug.Log("Handle Jabberwock death");
        }

        return finished;
    }

    public void JabberDefeated()
    {

        StartCoroutine(CutOffHead());
    }

    private IEnumerator CutOffHead()
    {
        GetComponent<Animator>().SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        WordManager.Instance.WinGame();
    }
}
