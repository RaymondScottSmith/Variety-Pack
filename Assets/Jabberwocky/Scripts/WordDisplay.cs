using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordDisplay : MonoBehaviour
{
    public TMP_Text text;

    [SerializeField] private float moveSpeed = 1f;
    private bool moving;

    private Animator animator;

    public JA_Word myWord;
    public void SetWord(string word, JA_Word jaWord)
    {
        text.text = word;
        moving = true;
        animator = GetComponent<Animator>();
        myWord = jaWord;
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
        Jabberwock jab = GetComponent<Jabberwock>();
        if (jab != null)
        {
            jab.JabberDefeated();
        }
        else
        {
            yield return new WaitForSeconds(.2f);
            animator.SetTrigger("Die");
        }
    }

    public void KillCreature()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            Vector3 newPosition = transform.position + Vector3.left * moveSpeed;
            transform.position = newPosition;
            if (transform.position.x <= WordManager.Instance.hero.transform.position.x)
            {
                WordManager.Instance.RemoveWord(myWord);
                WordManager.Instance.LoseGame();
                KillCreature();
            }
        }
        
    }
}
