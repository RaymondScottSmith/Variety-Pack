using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingThing : MonoBehaviour
{
    private Rigidbody2D myRigid;
    private bool parachute = true;
    [SerializeField] private float fallSpeed = 1f;

    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }
    public void StartFreefalling()
    {
        myRigid.gravityScale = 1f;
        parachute = false;
    }

    private void FixedUpdate()
    {
        if (parachute)
        {
            transform.Translate(Vector3.down * fallSpeed);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
