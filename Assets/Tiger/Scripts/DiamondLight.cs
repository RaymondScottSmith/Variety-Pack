using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondLight : MonoBehaviour
{
    public bool isLit;

    [SerializeField]
    private SpriteRenderer lightSprite;


    public void ActivateLight()
    {
        isLit = true;
        lightSprite.color = Color.yellow;
    }

    public void DeActivateLight()
    {
        isLit = false;
        lightSprite.color = Color.black;
    }
}
