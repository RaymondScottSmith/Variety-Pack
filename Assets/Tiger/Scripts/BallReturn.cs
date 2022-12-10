using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReturn : MonoBehaviour
{
    [SerializeField]
    private Transform ballSpawnLocation;

    [SerializeField] private TigerCage cage;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            PinballManager.Instance.DestroyBall(col.gameObject);
        }
    }
}
