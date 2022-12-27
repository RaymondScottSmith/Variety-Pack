using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sa_Interact : MonoBehaviour
{
    [Header("Variables")] public float pickupDistance = 10f;
    public LayerMask interactLayer;
    public Camera cam;

    public Transform holdPos;

    public bool carrying;

    public GameObject carried;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandleClicks();
    }

    private void HandleClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance, interactLayer))
            {
                switch (hit.collider.tag)
                {
                    case "Hold":
                        if (!carrying)
                            PickUpObject(hit.collider.gameObject);
                        else
                        {
                            SetDownObject(hit, carried);
                        }
                        break;
                    case "Surface":
                        if (carrying && carried != null)
                            SetDownObject(hit,carried);
                        break;
                    
                    case "Ball":
                        if (!carrying && carried == null)
                        {
                            PickUpObject(hit.collider.GetComponentInParent<Dispensor>().TakeItem());
                        }

                        break;
                    case "Note":
                        if (carrying && carried.GetComponent<Sa_Ingredient>() != null)
                        {
                            if (carried.GetComponent<Sa_Ingredient>().ingType == Sa_Ingredient.Ingredient.Bread)
                            {
                                Sa_Toaster toaster = hit.collider.GetComponent<Sa_Toaster>();
                                switch (toaster.toasterState)
                                {
                                    case Sa_Toaster.ToasterState.Empty:
                                        toaster.LoadToast();
                                        Destroy(carried.gameObject);
                                        carried = null;
                                        carrying = false;
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        else if (!carrying && carried == null)
                        {
                            Sa_Toaster toaster = hit.collider.GetComponent<Sa_Toaster>();
                            switch (toaster.toasterState)
                            {
                                case Sa_Toaster.ToasterState.Done:
                                    PickUpObject(toaster.TakeToast());
                                    break;

                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void PickUpObject(GameObject pickup)
    {
        Vector3 pickupScale = pickup.transform.lossyScale;
        pickup.transform.SetParent(null);
        pickup.transform.localScale = pickupScale;
        pickup.transform.SetParent(holdPos);
        pickup.transform.localPosition = Vector3.zero;
        pickup.transform.localRotation = Quaternion.Euler(Vector3.zero);
        //pickup.GetComponent<Rigidbody>().isKinematic = true;
        Rigidbody[] bodies = pickup.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in bodies)
        {
            body.isKinematic = true;
        }

        BoxCollider[] colliders = pickup.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider collider in colliders)
        {
            collider.enabled = false;
        }
        //pickup.GetComponent<BoxCollider>().enabled = false;
        carrying = true;
        carried = pickup;
    }

    void SetDownObject(RaycastHit hit, GameObject heldObject)
    {
        Vector3 pickupScale = heldObject.transform.lossyScale;
        heldObject.transform.SetParent(null);
        heldObject.transform.localScale = pickupScale;
        heldObject.transform.SetParent(hit.collider.transform);
        heldObject.transform.position = hit.point + Vector3.up * 0.1f;
        heldObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        //heldObject.GetComponent<Rigidbody>().isKinematic = false;
        //heldObject.GetComponent<BoxCollider>().enabled = true;
        Rigidbody[] bodies = heldObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in bodies)
        {
            body.isKinematic = false;
        }

        BoxCollider[] colliders = heldObject.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider collider in colliders)
        {
            collider.enabled = true;
        }
        carrying = false;
        carried = null;
    }
}
