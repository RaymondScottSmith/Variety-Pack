using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerJump pj = collision.gameObject.GetComponent<PlayerJump>();
            musicSource.Stop();
            StartCoroutine(pj.LoseGame());

        }
    }
}
