using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run_Move : MonoBehaviour
{
    private float speed;

    public bool reverseMove;

    public bool on = true;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = RunnerManager.Instance.moveSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.forward * RunnerManager.Instance.moveSpeed;
        if (reverseMove)
            transform.Translate(moveDirection) ;
        else
        {
            transform.Translate(-moveDirection) ;
        }
    }
}
