using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHole : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            PinballManager.Instance.BallInHole(col.gameObject);
        }
    }
}
