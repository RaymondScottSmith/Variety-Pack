using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Camera myCam;

    [SerializeField] private float paddleY = -3f;

    [SerializeField] private float maxX, minX;
    
    private float startXPos;
    private float startYPos;

    private bool isDragging = false;

    public bool notWaiting = true;

    private void Update()
    {
        if (isDragging)
        {
            DragObject();
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);

        startXPos = mousePos.x - transform.localPosition.x;
        startYPos = mousePos.y - transform.localPosition.y;

        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Note"))
        {
            StartCoroutine(ConfirmCollide(col));
        }
    }

    private IEnumerator ConfirmCollide(Collider2D col)
    {
        yield return new WaitForFixedUpdate();
        if (notWaiting && col != null)
        {
            RhythmManager.Instance.ScoreNormal(col.GetComponent<Note>().noteColor);
            col.GetComponent<Note>().SpindAndDie();
        }

        notWaiting = true;


    }

    public void DragObject()
    {
        Vector3 mousePos = Input.mousePosition;

        if(!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);
        transform.localPosition = new Vector3(Mathf.Clamp(mousePos.x - startXPos, minX, maxX), paddleY, transform.localPosition.z);
    }
}
