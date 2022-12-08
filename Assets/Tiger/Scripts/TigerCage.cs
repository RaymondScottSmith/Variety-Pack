using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerCage : MonoBehaviour
{
    private bool blockerOn;

    [SerializeField] private BoxCollider2D blocker;

    [SerializeField] private Sprite cage;
    [SerializeField] private Sprite brokenCage;

    [SerializeField] private SpriteRenderer cageRenderer;
    
    public void SetBlocker(bool blockerIsOn)
    {
        blockerOn = blockerIsOn;
        blocker.enabled = blockerOn;
        cageRenderer.sprite = blockerOn ? brokenCage : cage;
    }
    // Start is called before the first frame update
    void Start()
    {
        cageRenderer.sprite = cage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("collider: " + col.contacts[0].point.y);
        //Debug.Log("player: " + transform.position.y);
        if (!col.gameObject.CompareTag("Ball") || blockerOn)
        {
            return;
        }
        SetBlocker(true);


    }
}
