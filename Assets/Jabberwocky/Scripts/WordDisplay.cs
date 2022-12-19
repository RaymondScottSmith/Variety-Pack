using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordDisplay : MonoBehaviour
{
    public TMP_Text text;

    [SerializeField] private float fallSpeed = 1f;
    public void SetWord(string word)
    {
        text.text = word;
    }

    public void RemoveLetter()
    {
        text.text = text.text.Remove(0, 1);
        text.color = Color.red;
        
    }

    public void RemoveWord()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = transform.position + Vector3.left * fallSpeed;
        transform.position = newPosition;
    }
}
