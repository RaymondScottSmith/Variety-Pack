using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCollider : MonoBehaviour
{
    [SerializeField] private Sprite normalSprite, hitSprite;
    private SpriteRenderer mySpriteRenderer;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
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
