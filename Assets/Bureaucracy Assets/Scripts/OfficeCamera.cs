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

    public bool freeSwitch;

    private bool lookingDesk = false;

    public static OfficeCamera Instance;

    private Animator cameraAnimator;

    private AudioSource myAudioSource;


    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        cameraAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (BoxCollider col in boxColliders)
        {
            col.enabled = lookingDesk;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && freeSwitch)
        {
            lookingDesk = !lookingDesk;
            cameraAnimator.SetBool("DeskView", lookingDesk);
            
            if (lookingDesk)
                DialogueManager.Instance.HideCanvas();
            else
            {
                DialogueManager.Instance.ShowCanvas();
            }
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

    public void TurnOffMusic()
    {
        myAudioSource.Stop();
    }

    public void TurnOnMusic()
    {
        myAudioSource.Play();
    }
}
