using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{

    [SerializeField]
    private List<string> killTags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        foreach(string killTag in killTags)
        {
            if (collision.gameObject.CompareTag(killTag))
            {
                Destroy(collision.transform.parent.gameObject);
                break;
            }
        }
    }
}
