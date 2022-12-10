using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPoints : MonoBehaviour
{

    [SerializeField] private int pointsValue = 10;
    // Start is called before the first frame update
    public void ChangePoints()
    {
        PinballManager.Instance.ChangeScore(pointsValue);
    }
}
