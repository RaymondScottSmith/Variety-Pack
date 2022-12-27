using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispensor : MonoBehaviour
{
    public GameObject dispensedItem;


    public GameObject TakeItem()
    {
        GameObject newItem = Instantiate(dispensedItem);
        return newItem;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
