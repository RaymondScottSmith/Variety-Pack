using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run_Ghost : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public Animator ghostAnimator;
    public bool alive = true;

    public ParticleSystem deathSparkles;

    public MeshRenderer headMesh;
    public MeshCollider headColl;

    private AudioSource myAudio;

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive)
        {
            headMesh.enabled = true;
            headColl.enabled = true;
        }
        else
        {
            headColl.enabled = false;
            headMesh.enabled = false;
        }
        //Vector3 lookAt = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        transform.LookAt(player.transform);
        Vector3 rotateMe = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(0, rotateMe.y, rotateMe.z));
    }

    public void DeathAnimation()
    {
        myAudio.Play();
        deathSparkles.Play();
        alive = false;
    }
}
