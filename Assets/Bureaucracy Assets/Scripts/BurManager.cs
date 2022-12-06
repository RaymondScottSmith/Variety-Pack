using System.Collections;
using System.Collections.Generic;
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

    public static BurManager Instance;

    void Awake()
    {
        Instance = this;
    }

    
    // Start is called before the first frame update
    void Start()
    {
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
        if (currentLying)
        {
            //Code to handle failure here
        }
        else
        {
            Debug.Log("Hit Pass Button");
            currentCreature.Leave();
        }
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
