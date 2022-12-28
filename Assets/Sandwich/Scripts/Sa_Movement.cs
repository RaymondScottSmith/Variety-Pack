using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sa_Movement : MonoBehaviour
{
    [Header("Movement")] public float moveSpeed;
    public float groundDrag;

    [Header("Ground Check")] public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;

    public Animator armAnimator;

    private float horizontalInput, verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    public bool checkingMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f);

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void FixedUpdate()
    {
        if (!SandwichManager.Instance.checkingMenu)
            MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (SandwichManager.Instance.checkingMenu)
            return;
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        SpeedControl();
        
        //apply drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0f;
        }
        armAnimator.SetFloat("Velocity", rb.velocity.magnitude);
    }
}
