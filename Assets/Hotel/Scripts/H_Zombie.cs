using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class H_Zombie : MonoBehaviour
{
    [Header("Zombie Stats")] [SerializeField]
    private float detectionRange = 15;
    [SerializeField]
    private float speed = 6, acceleration = 6;

    [SerializeField] private int maxHealth = 100, attackDamage = 20;
    private int health;

    [SerializeField] private float disappearTime = 6f, attackDistance, attackRecharge = 1f;

    [Header("Zombie Audio")] [SerializeField]
    private AudioClip[] deathMoans;

    [SerializeField] private AudioClip[] detectSounds;

    [SerializeField] private AudioClip[] painSounds;
    [SerializeField] private AudioClip[] attackSounds;

    [SerializeField] private AudioClip hitSound,missSound;

    [Header("Children GameObjects")] [SerializeField]
    private Transform raycastPoint;

    [SerializeField] private CapsuleCollider capCollider;

    [SerializeField] private AudioSource attackAudio;
        
    //Component references
    private NavMeshAgent zombieNav;
    private SphereCollider detector;
    private Animator animator;
    private AudioSource audioSource;
    private Outline myOutline;
    
    //AI
    private bool detectPlayer;
    private bool playerInRange;
    private GameObject player;
    private bool isDead;
    private bool isAttacking;

    void Awake()
    {
        zombieNav = GetComponent<NavMeshAgent>();
        detector = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        myOutline = GetComponent<Outline>();
        health = maxHealth;

    }
    // Start is called before the first frame update
    void Start()
    {
        detectPlayer = false;
        isAttacking = false;
        isDead = false;
        zombieNav.speed = speed;
        zombieNav.acceleration = acceleration;
        detector.radius = detectionRange;
    }

    public void PlayerInRange(bool isPlayer)
    {
        playerInRange = isPlayer;
    }

    public void TakeDamage(int damage)
    {
        //StartCoroutine(FlashOutline(0.5f));
        if (!playerInRange)
            playerInRange = true;
        animator.SetTrigger("Hurt");
        health -= damage;
        
        if (health < 0)
            health = 0;
        animator.SetInteger("Health", health);
        if (health == 0)
        {
            Debug.Log("Zombie Die");
            isDead = true;
            capCollider.enabled = false;
            GetComponent<Rigidbody>().freezeRotation = true;
            zombieNav.velocity = Vector3.zero;
            zombieNav.isStopped = true;
        }
    }

    public IEnumerator FlashOutline(float duration)
    {
        myOutline.OutlineWidth = 1f;
        yield return new WaitForSeconds(duration);
        myOutline.OutlineWidth = 0f;
    }

    public void ResumeMoving()
    {
        if (!isDead)
            zombieNav.isStopped = false;
    }

    public void StopMoving()
    {
        zombieNav.isStopped = true;
        zombieNav.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (playerInRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(raycastPoint.position,   player.transform.position - raycastPoint.position,
                    out hit, 1000f))
            {
                //Debug.Log(hit.collider.tag);
                if (hit.collider.CompareTag("Player"))
                {
                    player = hit.collider.gameObject;
                    if (!detectPlayer)
                        PlayRandomSound(detectSounds);
                    detectPlayer = true;
                }
            }
            
        }
    }

    private void PlayRandomSound(AudioClip[] sounds)
    {
        if (sounds.Length > 0)
        {
            int randSound = Random.Range(0, sounds.Length);
            audioSource.PlayOneShot(sounds[randSound]);
        }
        else
        {
            Debug.Log("Need to add AudioClips");
        }
    }

    public void PlayHurtSound()
    {
        PlayRandomSound(painSounds);
    }

    public void DeathSequence()
    {
        PlayRandomSound(deathMoans);
        
    }

    public void RemoveSequence()
    {
        Invoke("Remove", disappearTime);
    }

    private void Remove()
    {
        Destroy(gameObject);
    }
    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        if (detectPlayer)
        {
            zombieNav.SetDestination(player.transform.position);
            if (!isAttacking && Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
            {
                StartCoroutine(MakeAttack());
            }
        }
        animator.SetFloat("Velocity", zombieNav.velocity.magnitude);
    }

    private IEnumerator MakeAttack()
    {
        transform.LookAt(player.transform);
        PlayRandomSound(attackSounds);
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackRecharge);
        isAttacking = false;
    }

    public void AttackHitOrMiss()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance && !isDead)
            {
                attackAudio.PlayOneShot(hitSound);
                player.GetComponent<H_PlayerNavMesh>().TakeDamage(attackDamage);
            }
            else
            {
                attackAudio.PlayOneShot(missSound);
                //Debug.Log("Zombie Attack Misses");
            }
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player = col.gameObject;
            PlayerInRange(true);
        }
    }

    /*
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player = null;
            PlayerInRange(false);
        }
    }
    */
    
}
