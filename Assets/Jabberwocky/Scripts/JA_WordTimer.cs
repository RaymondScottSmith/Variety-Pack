using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JA_WordTimer : MonoBehaviour
{
    public WordManager wordManager;

    public float wordDelay = 1.5f;

    private float nextWordTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        wordManager = GetComponent<WordManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= nextWordTime)
        {
            wordManager.AddWord();
            nextWordTime = Time.time + wordDelay;
            //So that the spawn delay slowly gets faster and faster
            wordDelay *= 0.99f;
        }
    }
}
