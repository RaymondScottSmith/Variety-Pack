using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flipper : MonoBehaviour
{

    public bool leftFlipper;

    private AudioSource myAudioSource;

    private bool hasSounded;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource= GetComponent<AudioSource>();
        hasSounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool leftInput = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftShift);
        bool rightInput = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightShift);

        if ((leftFlipper && leftInput) || (!leftFlipper && rightInput))
        {
            if (!hasSounded)
            {
                hasSounded = true;
                myAudioSource.Play();
            }
            GetComponent<HingeJoint2D>().useMotor = true;
        }
        else
        {
            hasSounded = false;
            GetComponent<HingeJoint2D>().useMotor = false;
        }
        
        /*
        if ((leftFlipper && Input.GetKey(KeyCode.LeftArrow)) || (!leftFlipper && Input.GetKey(KeyCode.RightArrow)))
        {
            if (!hasSounded)
            {
                hasSounded = true;
                myAudioSource.Play();
            }
            GetComponent<HingeJoint2D>().useMotor = true;
        }
        else
        {
            hasSounded = false;
            GetComponent<HingeJoint2D>().useMotor = false;
        }
        
        */
    }
}
