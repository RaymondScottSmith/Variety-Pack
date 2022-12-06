using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OfficeCamera : MonoBehaviour
{

    [SerializeField] private Vector3 cameraPosition1;
    
    [SerializeField] private Vector3 cameraPosition2;

    [SerializeField] private Vector3 cameraRotation1;
    [SerializeField] private Vector3 cameraRotation2;

    [SerializeField] private AnimatedBookController bookController;

    [SerializeField] private List<BoxCollider> boxColliders;

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
        foreach (BoxCollider col in boxColliders)
        {
            col.enabled = lookingDesk;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lookingDesk = !lookingDesk;
            cameraAnimator.SetBool("DeskView", lookingDesk);
        }

        if (lookingDesk && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            bookController.TurnToPreviousPage();
        }
        if (lookingDesk && (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow)))
        {
            bookController.TurnToNextPage();
        }
    }

    public void SwitchCamera()
    {
        lookingDesk = !lookingDesk;
        cameraAnimator.SetBool("DeskView", lookingDesk);
    }
}
