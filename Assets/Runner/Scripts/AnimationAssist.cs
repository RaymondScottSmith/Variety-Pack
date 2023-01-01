using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAssist : MonoBehaviour
{
    private Animator assisted;

    void Awake()
    {
        assisted = GetComponent<Animator>();
    }
    
}
