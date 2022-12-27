using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sa_Holdable : MonoBehaviour
{

    public bool beingHeld;

    public Transform holdPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (beingHeld && holdPosition != null)
        {
            transform.position = holdPosition.position;
        }
    }
}
