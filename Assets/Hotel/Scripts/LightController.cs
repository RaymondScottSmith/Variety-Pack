using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private Light sceneLight;
    // Start is called before the first frame update
    void Start()
    {
        sceneLight = GetComponent<Light>();
        int layerMask = 1 << 7;
        sceneLight.cullingMask = layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
