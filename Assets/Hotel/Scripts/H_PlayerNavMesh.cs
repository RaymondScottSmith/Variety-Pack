using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class H_PlayerNavMesh : MonoBehaviour
{

    [SerializeField] private GameObject moveTarget;
    private NavMeshAgent navMeshAgent;
    private Camera cam;
    private Animator animator;
    private AudioSource myAudio;
    private bool shooting;
    
    [SerializeField]
    private Transform raycastPoint;

    [SerializeField] private MultiAimConstraint pistolConstraint;

    [Header("Firearm Delays")] [SerializeField] private float pistolDelay = 1f;

    [Header("Firearm Animators")] [SerializeField]
    private Animator pistolAnim;

    [Header("Firearm Sounds")] [SerializeField]
    private AudioClip pistolSound;
    
    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        animator = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) 
        { 
            //a ray from the near plane of the camera to the NavMesh
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //if the ray hits any object that has NavMesh, Set agent Destination to that point
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                switch (hit.collider.tag)
                {
                    case "Floor":
                        if (shooting)
                        {
                            return;
                        }
                        navMeshAgent.isStopped = false;
                        navMeshAgent.SetDestination(hit.point);
                        StartCoroutine(FlashTarget(hit.point));
                        break;
                    case "Enemy":
                        navMeshAgent.velocity = Vector3.zero;
                        navMeshAgent.isStopped = true;
                        transform.LookAt(hit.collider.transform);
                        transform.Rotate(0f,-20f,0f);
                        RaycastHit gunHit;
                        
                        // Bit shift the index of the layer (7) to get a bit mask
                        int layerMask = 1 << 7;

                        // This would cast rays only against colliders in layer 7.
                        // But instead we want to collide against everything except layer 7. The ~ operator does this, it inverts a bitmask.
                        layerMask = ~layerMask;
                        if (Physics.Raycast(raycastPoint.position,   hit.collider.transform.position - raycastPoint.position,
                                out gunHit, 1000f, layerMask))
                        {
                            Debug.Log(gunHit.collider.tag);
                            if (gunHit.collider.CompareTag("Enemy"))
                                animator.SetTrigger("ShootPistol");
                            shooting = true;
                            StartCoroutine(DelayForFirearms(pistolDelay, hit.collider.transform));
                        }
                        break;
                    default:
                        break;
                }
                
            }
                
        }
        animator.SetFloat("Velocity", navMeshAgent.velocity.magnitude);
    }

    private IEnumerator DelayForFirearms(float delay, Transform nowLook = null)
    {
        yield return new WaitForSeconds(delay);
        shooting = false;
        if (nowLook != null)
        {
            transform.LookAt(nowLook);
        }
    }

    private IEnumerator FlashTarget(Vector3 position)
    {
        moveTarget.transform.position = (position + Vector3.up * .1f);
        moveTarget.SetActive(true);
        yield return new WaitForSeconds(1);
        moveTarget.SetActive(false);
    }

    public void ShootPistol()
    {
        Debug.Log("Pistol Sound Here");
        myAudio.PlayOneShot(pistolSound);
        pistolAnim.SetTrigger("Fire");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //navMeshAgent.destination = movePositionTransform.position;
    }
}
