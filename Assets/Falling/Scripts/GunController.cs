using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform target, firePoint;

    [SerializeField] private GameObject bullet;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private float reloadTime = 1f;

    private AudioSource myAudio;

    [SerializeField]
    private List<Animator> muzzleFlashes;

    private bool reloading;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FallGameManager.Instance.isRunning)
        {
            return;
        }
        transform.up = target.position - transform.position;
        if (!reloading && Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, firePoint.position,transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
            foreach (Animator anim in muzzleFlashes)
            {
                anim.SetTrigger("Fire");
            }
            myAudio.Play();
            StartCoroutine(ReloadGun());
        }
    }

    private IEnumerator ReloadGun()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}
