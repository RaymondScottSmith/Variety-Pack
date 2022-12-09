using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBounce : MonoBehaviour
{
    [SerializeField] private float ricochetForce = 10f;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector2 angle = col.GetContact(0).normal;
            rb.AddForce(angle * ricochetForce, ForceMode2D.Impulse);
        }
    }
}
