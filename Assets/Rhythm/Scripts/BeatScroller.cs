using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [SerializeField] private float beatTempo = 120f;

    private float adjustedTempo;

    private bool hasStarted;
    // Start is called before the first frame update
    void Start()
    {
        hasStarted = true;
        adjustedTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown)
            {
                hasStarted = true;
            }
        }
        else
        {
            transform.position -= new Vector3(0f, (adjustedTempo * Time.deltaTime), 0f);
        }
    }
}
