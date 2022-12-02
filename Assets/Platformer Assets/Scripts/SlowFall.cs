using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFall : MonoBehaviour
{

    [SerializeField]
    private float gravityValue = 0.05f;

    [SerializeField]
    private float mass = 50f;

    private Rigidbody2D myRigidbody;

    [SerializeField] private float maxFallSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponentInParent<Rigidbody2D>();
        GetComponentInParent<Rigidbody2D>().gravityScale = gravityValue;
        GetComponentInParent<Rigidbody2D>().mass = mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myRigidbody.velocity.y < maxFallSpeed)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, maxFallSpeed);
        }
        float zAngle = transform.parent.rotation.eulerAngles.z;
        if (zAngle >= 90 && zAngle < 270)
        {
            transform.localEulerAngles = new Vector3(0, 0, 180);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
