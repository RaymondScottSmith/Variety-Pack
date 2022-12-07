using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flipper : MonoBehaviour
{

    public bool leftFlipper;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((leftFlipper && Input.GetKey(KeyCode.LeftArrow)) || (!leftFlipper && Input.GetKey(KeyCode.RightArrow)))
        {
            GetComponent<HingeJoint2D>().useMotor = true;
        }
        else
        {
            GetComponent<HingeJoint2D>().useMotor = false;
        }
    }
}
