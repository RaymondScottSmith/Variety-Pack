using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCollider : MonoBehaviour
{
    [SerializeField] private Sprite normalSprite, hitSprite;
    private SpriteRenderer mySpriteRenderer;

    [SerializeField] private int scoreValue = 10;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateScore()
    {
        PinballManager.Instance.ChangeScore(scoreValue);
    }

    public void ShowHitSprite()
    {
        mySpriteRenderer.sprite = hitSprite;
    }

    public void ShowNormalSprite()
    {
        mySpriteRenderer.sprite = normalSprite;
    }
}
