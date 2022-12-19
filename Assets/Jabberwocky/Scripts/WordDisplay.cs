using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordDisplay : MonoBehaviour
{
    public TMP_Text text;

    [SerializeField] private float moveSpeed = 1f;
    private bool moving;
    public void SetWord(string word)
    {
        text.text = word;
        moving = true;
    }

    public void RemoveLetter()
    {
        text.text = text.text.Remove(0, 1);
        text.color = Color.red;
        
    }

    public void RemoveWord()
    {
        WordManager.Instance.SlashCreature(transform.position,null);
        moving = false;
        StartCoroutine(StopAndDie());
    }

    private IEnumerator StopAndDie()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            Vector3 newPosition = transform.position + Vector3.left * moveSpeed;
            transform.position = newPosition;
        }
        
    }
}
