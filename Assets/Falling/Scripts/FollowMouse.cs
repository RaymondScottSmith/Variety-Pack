using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Transform origin;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FallGameManager.Instance.isRunning)
        {
            return;
        }
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint) ;
        transform.position = worldPoint;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, origin.position);
    }
}
