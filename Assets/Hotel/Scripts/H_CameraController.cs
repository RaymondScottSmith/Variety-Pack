using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private H_PlayerNavMesh player;

    [SerializeField] private Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = startingPosition;
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - player.transform.position;
        //Debug.Log(offset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void LateUpdate () 
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }
}
