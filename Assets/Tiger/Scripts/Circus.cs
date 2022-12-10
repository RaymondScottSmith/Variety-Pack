using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Circus : MonoBehaviour
{
    [SerializeField] private List<GameObject> scratches;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject scratch in scratches)
        {
            scratch.SetActive(false);
        }
    }

    public void RemoveScratches()
    {
        foreach (GameObject scratch in scratches)
        {
            scratch.SetActive(false);
        }
    }

    [ContextMenu("Damage Circus")]
    public void AddScratch()
    {
        foreach (GameObject scratch in scratches)
        {
            if (!scratch.activeSelf)
            {
                scratch.SetActive(true);
                return;
            }
        }

        PinballManager.Instance.CircusDestroyed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
