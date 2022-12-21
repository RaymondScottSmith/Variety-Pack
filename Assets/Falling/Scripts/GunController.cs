using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform target, firePoint;

    [SerializeField] private GameObject bullet;

    [SerializeField] private float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = target.position - transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, firePoint.position, bullet.transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
