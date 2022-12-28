using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sa_ServeArea : MonoBehaviour
{
    private List<Sa_Ingredient> ingToServe;
    public float victoryTime = 1.5f;

    public List<Sa_Ingredient.Ingredient> orderedSand;

    private bool m_Started;

    public LayerMask m_LayerMask;

    [Header("Audio Clips")] 
    [SerializeField]
    private AudioClip goodSandwich;

    private Animator animator;
    private AudioSource myAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        ingToServe = new List<Sa_Ingredient>();
        m_Started = true;
        animator = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + Vector3.up/4, Vector3.one/2f, Quaternion.identity, m_LayerMask);
        if (CheckForSandwich() && orderedSand.Count != 0)
        {
            orderedSand.Clear();
            StartCoroutine(Success());
            
        }
    }

    private IEnumerator Success()
    {
        myAudio.PlayOneShot(goodSandwich);
        animator.SetTrigger("Flash");
        Sa_Ingredient[] ingsOnSquare = GetComponentsInChildren<Sa_Ingredient>();
        foreach (Sa_Ingredient ing in ingsOnSquare)
        {
            ing.gameObject.tag = "Player";
        }
        yield return new WaitForSeconds(victoryTime);
        
        foreach (Sa_Ingredient ing in ingsOnSquare)
        {
            Destroy(ing.gameObject);
        }
            
        SandwichManager.Instance.SandwichCompleted();
    }

    public void SetNewSandwich(Sandwich newSand)
    {
        if (orderedSand.Count == 0)
        {
            foreach (Sa_Ingredient.Ingredient ing in newSand.ingredients)
            {
                orderedSand.Add(ing);
            }
        }
    }

    bool CheckForSandwich()
    {
        Sa_Ingredient[] ingsOnSquare = GetComponentsInChildren<Sa_Ingredient>();
        List<Sa_Ingredient.Ingredient> tempList = new List<Sa_Ingredient.Ingredient>();
        
        foreach (Sa_Ingredient ing in ingsOnSquare)
        {
            tempList.Add(ing.ingType);
        }

        bool foundIng = false;
        foreach (Sa_Ingredient.Ingredient ing in orderedSand)
        {
            foundIng = false;
            foreach (Sa_Ingredient.Ingredient tempIng in tempList)
            {
                foundIng = false;
                if (tempIng == ing)
                {
                    tempList.Remove(ing);
                    foundIng = true;
                    break;
                }
            }

            if (foundIng == false)
            {
                return false;
            }
        }

        return true;

    }
    
    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position + Vector3.up/4, Vector3.one/2f);
    }
    */
    
    /*
    void OnTriggerEnter(Collider col)
    {
        Sa_Ingredient newIng = col.GetComponent<Sa_Ingredient>();
        if (newIng != null)
        {
            ingToServe.Add(newIng);
        }

        if (CheckCorrectSand())
        {
            Debug.Log("Correct Sandwich");
        }
        else
        {
            Debug.Log("Incorrect Sandwich");
        }
    }
    */
    void OnTriggerExit(Collider col)
    {
        Debug.Log("Detect Exit");
        Sa_Ingredient newIng = col.GetComponent<Sa_Ingredient>();
        if (newIng != null && ingToServe.Contains(newIng))
        {
            ingToServe.Remove(newIng);
        }
    }

    private bool CheckCorrectSand()
    {
        List<Sa_Ingredient.Ingredient> ingOnPlate = new List<Sa_Ingredient.Ingredient>();
        foreach (Sa_Ingredient ing in ingToServe)
        {
            ingOnPlate.Add(ing.ingType);
        }

        List<Sa_Ingredient.Ingredient> ingNeeded = new List<Sa_Ingredient.Ingredient>();
        foreach (Sa_Ingredient.Ingredient ing in orderedSand)
        {
            ingNeeded.Add(ing);
        }

        foreach (Sa_Ingredient.Ingredient ingPlate in ingOnPlate)
        {
            //if (ingNeeded.Con)
        }

        return false;
    }
    
    
}
