using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoemDisplay : MonoBehaviour
{
    [Header("Poems")]
    [SerializeField] private TMP_Text poemText, titleText,authorText;

    [TextArea] public string poem1, poem2, poem3, poem4, poem5, poem6, poem7, poem8, poem9, poem10;

    public List<string> poemNames;
    public List<string> poemAuthors;
    private List<string> poems;

    void Awake()
    {
        poems = new List<string>();
        poems.Add(poem1);
        poems.Add(poem2);
        poems.Add(poem3);
        poems.Add(poem4);
        poems.Add(poem5);
        poems.Add(poem6);
        poems.Add(poem7);
        poems.Add(poem8);
        poems.Add(poem9);
        poems.Add(poem10);
    }
    public void UpdateText(int poemNumber)
    {
        poemText.text = poems[poemNumber];
        titleText.text = poemNames[poemNumber];
        authorText.text = "By " + poemAuthors[poemNumber];
    }
    
}
