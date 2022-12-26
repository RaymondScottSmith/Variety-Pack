using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class H_PlayerNavMesh : MonoBehaviour
{

    [Header("Player Stats")] [SerializeField]
    private int maxHealth = 100;

    private int health;
    
    [SerializeField] private GameObject moveTarget;
    [SerializeField] private Slider healthBar;
    private NavMeshAgent navMeshAgent;
    private Camera cam;
    private Animator animator;
    private AudioSource myAudio;
    private bool shooting;
    private bool isDead;
    private CapsuleCollider capCollider;
    private H_Interactive selectedInteractable;
    
    [SerializeField]
    private Transform raycastPoint;

    [SerializeField] private MultiAimConstraint pistolConstraint;

    [Header("Firearm Delays")] [SerializeField] private float pistolDelay = 1f;

    [Header("Firearm Damage")] [SerializeField]
    private int pistolDamage = 40;

    [Header("Firearm Animators")] [SerializeField]
    private Animator pistolAnim;

    [Header("Firearm Sounds")] [SerializeField]
    private AudioClip pistolSound;


    [SerializeField]
    private AudioClip[] painSounds;

    [SerializeField] private AudioClip deathSound;
    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        animator = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
        capCollider = GetComponent<CapsuleCollider>();
        health = maxHealth;
        isDead = false;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        healthBar.value = (float)health/(float)(maxHealth);
        if (isDead)
        {
            return;
        }
        if (Input.GetMouseButtonUp(0)) 
        { 
            int mouseMask = 1 << 12;

            // This would cast rays only against colliders in layer 12.
            // But instead we want to collide against everything except layer 12. The ~ operator does this, it inverts a bitmask.
            mouseMask = ~mouseMask;
            //a ray from the near plane of the camera to the NavMesh
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //if the ray hits any object that has NavMesh, Set agent Destination to that point
            if (Physics.Raycast(ray, out RaycastHit hit,1000, mouseMask))
            {
                //Debug.Log(hit.collider.gameObject.tag);
                if (selectedInteractable != null)
                {
                    selectedInteractable.CancelSelect();
                }
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
                        
                        RaycastHit gunHit;
                        
                        // Bit shift the index of the layer (7) to get a bit mask
                        int layerMask = 1 << 8;
                        int layerMask2 = 1 << 12;
                        layerMask = layerMask | layerMask2;
                        // This would cast rays only against colliders in layer 7.
                        // But instead we want to collide against everything except layer 7. The ~ operator does this, it inverts a bitmask.
                        layerMask = ~layerMask;
                        if (Physics.Raycast(raycastPoint.position,   hit.collider.transform.position - raycastPoint.position,
                                out gunHit, 1000f, layerMask) && !shooting)
                        {
                            Debug.Log(gunHit.collider.tag);
                            //Debug.Log(gunHit.collider.tag);
                            if (gunHit.collider.CompareTag("Enemy"))
                            {
                                StartCoroutine(gunHit.collider.GetComponentInParent<H_Zombie>().FlashOutline(0.5f));
                                animator.SetTrigger("ShootPistol");
                                StartCoroutine(DamageEnemy(gunHit.collider.gameObject.GetComponentInParent<H_Zombie>(), pistolDamage, pistolDelay));
                            }
                                
                            shooting = true;
                            StartCoroutine(DelayForFirearms(pistolDelay, hit.collider.transform));
                        }
                        break;
                    
                    case "Interact":
                        selectedInteractable = hit.collider.GetComponent<H_Interactive>();
                        selectedInteractable.MoveToInteract(this.gameObject);
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
        if (nowLook != null)
        {
            Vector3 temp = nowLook.position - raycastPoint.position;
            Quaternion tempQuat = Quaternion.LookRotation(temp, Vector3.up);
        
            if (!CompareFloats(tempQuat.eulerAngles.y - 20f, transform.rotation.eulerAngles.y, 1f))
            {
                transform.LookAt(nowLook);
                transform.Rotate(0f,-20f,0f);
            }
        }
        
        yield return new WaitForSeconds(delay);
        
        shooting = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;
        myAudio.pitch = 1.35f;
        myAudio.PlayOneShot(painSounds[Random.Range(0,painSounds.Length)]);
        animator.SetTrigger("GetHit");
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }

        if (health == 0)
        {
            isDead = true;
            capCollider.enabled = false;
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.isStopped = true;
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        animator.SetBool("Dead", isDead);
    }

    public void PlayDeathSound()
    {
        myAudio.pitch = 1.35f;
        myAudio.PlayOneShot(deathSound);
    }

    public void LoseGame()
    {
        H_GameManager.Instance.LoseGame();
    }

    private IEnumerator DamageEnemy(H_Zombie zombie, int damageValue, float delay)
    {
        yield return new WaitForSeconds(delay / 2f);
        zombie.TakeDamage(damageValue, this.gameObject);
        yield return new WaitForFixedUpdate();
    }

    private bool CompareFloats(float value1, float value2, float margin = 0.01f)
    {
        return Mathf.Abs(value1 - value2) <= margin;
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
        myAudio.pitch = 1f;
        myAudio.PlayOneShot(pistolSound);
        pistolAnim.SetTrigger("Fire");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //navMeshAgent.destination = movePositionTransform.position;
    }
}
