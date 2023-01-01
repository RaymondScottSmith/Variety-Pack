using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Sa_MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(cameraPosition.position);
        transform.position = cameraPosition.position;
    }
    
    //[MenuItem("Help/Hide Flags/Show All Objects")]
    private static void ShowAll()
    {
        var allGameObjects = Object.FindObjectsOfType<GameObject>();
        foreach (var go in allGameObjects)
        {
            switch (go.hideFlags)
            {
                case HideFlags.HideAndDontSave:
                    go.hideFlags = HideFlags.DontSave;
                    break;
                case HideFlags.HideInHierarchy:
                case HideFlags.HideInInspector:
                    go.hideFlags = HideFlags.None;
                    break;
            }
        }
    }
}
