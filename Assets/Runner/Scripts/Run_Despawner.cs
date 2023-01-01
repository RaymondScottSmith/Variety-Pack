using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run_Despawner : MonoBehaviour
{
    public static Run_Despawner Instance;
    public string doorTag = "Door";
    public string targetTag;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(doorTag))
        {
            Animator anim = col.GetComponent<Animator>();
            anim.Rebind();
            anim.Update(0f);
            col.gameObject.SetActive(false);
            Run_Spawner.Instance.ReturnDoorToPool(col.gameObject);
        }

        if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.gameObject.CompareTag(targetTag))
        {
            col.gameObject.transform.root.gameObject.SetActive(false);
            Run_Spawner.Instance.ReturnTargetToPool(col.gameObject.transform.root.gameObject);
        }
    }
}
