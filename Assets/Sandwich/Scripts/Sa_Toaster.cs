using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sa_Toaster : MonoBehaviour
{
    private Animator myAnimator;
    [SerializeField] private float toastTime = 3f;

    public ToasterState toasterState;

    public GameObject toastPrefab;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        toasterState = ToasterState.Empty;
    }

    public void LoadToast()
    {
        if (toasterState == ToasterState.Empty)
            StartCoroutine(ToastBread());
        
    }

    private IEnumerator ToastBread()
    {
        toasterState = ToasterState.Cooking;
        myAnimator.SetTrigger("Load");
        yield return new WaitForSeconds(toastTime);
        toasterState = ToasterState.Done;
        myAnimator.SetTrigger("Done");
    }

    public GameObject TakeToast()
    {
        if (toasterState == ToasterState.Done)
        {
            toasterState = ToasterState.Empty;
            myAnimator.SetTrigger("Waiting");
            return Instantiate(toastPrefab);
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum ToasterState
    {
        Empty,
        Cooking,
        Done
    }
}
