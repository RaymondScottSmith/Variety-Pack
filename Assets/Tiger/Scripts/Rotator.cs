using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    [SerializeField] private float rotationSpeed = 1f;

    public bool rotating;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetSpeed(float speed)
    {
        rotationSpeed = speed;
    }

    void FixedUpdate()
    {
        if (rotating)
            gameObject.transform.Rotate(new Vector3(0,0,rotationSpeed)); //applying rotation.
        
    }
}
