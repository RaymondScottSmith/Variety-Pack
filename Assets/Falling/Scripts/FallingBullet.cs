using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMe",3.5f);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Jumpable"))
            Destroy(gameObject);
    }
}
