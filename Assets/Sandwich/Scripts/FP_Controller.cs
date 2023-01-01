using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Controller : MonoBehaviour
{
    public float sensX, sensY;

    public Transform orientation;

    private float xRotation, yRotation;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        
        //rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);

    }
}
