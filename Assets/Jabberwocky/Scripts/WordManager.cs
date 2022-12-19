using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public List<JA_Word> words;


    private void Start()
    {
        AddWord();
        AddWard();
        AddWord();
    }

    public void AddWord()
    {
        JA_Word word = new JA_Word("example");
    }
    
}
