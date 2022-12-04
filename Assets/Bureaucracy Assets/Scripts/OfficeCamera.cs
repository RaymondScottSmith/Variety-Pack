using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeCamera : MonoBehaviour
{

    [SerializeField] private Vector3 cameraPosition1;
    
    [SerializeField] private Vector3 cameraPosition2;

    [SerializeField] private Vector3 cameraRotation1;
    [SerializeField] private Vector3 cameraRotation2;

    private bool lookingDesk = false;

    private Animator cameraAnimator;
    // Start is called before the first frame update
    void Start()
    {
        cameraAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lookingDesk = !lookingDesk;
            cameraAnimator.SetBool("DeskView", lookingDesk);
        }
    }
}
