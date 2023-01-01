using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run_Repeat : MonoBehaviour
{
    private Vector3 startPos;

    private float repeatWidth;

    public GameObject floor;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.z / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.z < startPos.z - repeatWidth)
        {
            transform.position = startPos;
        }
    }

    public void SetupFall()
    {
        floor.SetActive(false);
    }
}
