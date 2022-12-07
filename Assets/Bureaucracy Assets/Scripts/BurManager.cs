using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
//using Debug = System.Diagnostics.Debug;

public class BurManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> creaturePrefabs;

    [SerializeField] private List<CreatureInfo> creatures;

    [SerializeField] private int testCreatureNumber;
    
    public TMP_Text commScreen;

    public SeaCreature currentCreature;

    public bool currentLying;

    [SerializeField] private GameObject dolphinPrefab;

    public static BurManager Instance;

    public GameObject net;

    [SerializeField] private Animator lightAnimator;
    void Awake()
    {
        Instance = this;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        foreach (CreatureInfo creature in creatures)
        {
            DialogueManager.Instance.dolphinNames.AddRange(creature.validNames);
        }
        
        SpawnNewCreature();
    }

    public void SpawnNewCreature()
    {
        int creatureNum = Random.Range(0, creatures.Count);
        /*
        SeaCreature creature = creaturePrefabs[creatureNum].GetComponent<SeaCreature>();
        creature.AssignAnswers(false, "Quade", CreatureType.Whale, "I love cheerios.");
        Instantiate(creature.gameObject, creature.startPosition, creature.transform.rotation);
        */
        
        currentCreature = creatures[creatureNum].creaturePrefab.GetComponent<SeaCreature>();
        currentCreature = Instantiate(currentCreature.gameObject, currentCreature.startPosition, currentCreature.transform.rotation).GetComponent<SeaCreature>();
        DialogueManager.Instance.EnterDialogueMode(creatures[creatureNum], currentLying);
        
        
        string randomName = creatures[creatureNum].validNames[Random.Range(0, creatures[creatureNum].validNames.Count)];
        string randomFood = creatures[creatureNum].validFoods[Random.Range(0, creatures[creatureNum].validFoods.Count)];
        currentCreature.AssignAnswers(false, randomName, creatures[creatureNum].creatureType, randomFood);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitPassButton()
    {
        DialogueManager.Instance.HideCanvas();
        if (currentLying)
        {
            StartCoroutine(DolphinTaunt());
            Debug.Log("Handle Failure Here");
            //Code to handle failure here
        }
        else
        {
            Debug.Log("Hit Pass Button");
            StartCoroutine(CreatureLeave());
        }
    }

    public void HitNetButton()
    {
        DialogueManager.Instance.HideCanvas();
        if (currentLying)
        {
            StartCoroutine(CatchDolphin());
            //Add code to catch dolphin
        }
        else
        {
            StartCoroutine(AngryCreatureLeave());
            Debug.Log("Add failure consequence for false accusation here.");
        }
    }

    private IEnumerator CatchDolphin()
    {
        
        Destroy(currentCreature.gameObject);
        currentCreature = Instantiate(dolphinPrefab).GetComponent<SeaCreature>();
        currentCreature.isDolphin = true;
        yield return new WaitForSeconds(1f);
        net.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        commScreen.text = "Drat! You caught me. Curse you Tuna!";
        yield return new WaitForSeconds(3f);
        Debug.Log("Add code for win condition");
    }
    
    private IEnumerator AngryCreatureLeave()
    {
        lightAnimator.SetTrigger("Light");
        commScreen.text = "How Dare You! You'll be hearing from my attorneys!";
        yield return new WaitForSeconds(2f);
        currentCreature.Leave();
    }

    private IEnumerator DolphinTaunt()
    {
        
        Destroy(currentCreature.gameObject);
        currentCreature = Instantiate(dolphinPrefab).GetComponent<SeaCreature>();
        currentCreature.isDolphin = true;
        //currentCreature.arrived = true;
        commScreen.text = "Hah! You fell for it you stupid Tuna!";
        yield return new WaitForSeconds(3f);
        currentCreature.Leave();
    }

    private IEnumerator CreatureLeave()
    {
        commScreen.text = "Thanks. Now I'm late!";
        yield return new WaitForSeconds(1f);
        currentCreature.Leave();
    }

    public void AskName()
    {
        commScreen.text = "My name is " +
                          currentCreature.CreatureName()
                          + ". Pleased to meet you.";
    }

    public void AskFood()
    {
        commScreen.text = "I happen to enjoy the occasional "
                          + currentCreature.FactOne() +
                          ". When I'm in the mood.";
    }


}

public enum CreatureType
{
    Whale,
    Dolphin,
    Dugong,
    GreatWhite,
    Hammerhead,
    MantaRay,
    SeaTurtle,
    Sunfish,
    Swordfish
}
