using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BurManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> creaturePrefabs;

    [SerializeField] private List<CreatureInfo> creatures;

    [SerializeField] private int testCreatureNumber;
    
    public TMP_Text commScreen;

    public SeaCreature currentCreature;

    
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewCreature(testCreatureNumber);
    }

    public void SpawnNewCreature(int creatureNum)
    {
        /*
        SeaCreature creature = creaturePrefabs[creatureNum].GetComponent<SeaCreature>();
        creature.AssignAnswers(false, "Quade", CreatureType.Whale, "I love cheerios.");
        Instantiate(creature.gameObject, creature.startPosition, creature.transform.rotation);
        */

        currentCreature = creatures[0].creaturePrefab.GetComponent<SeaCreature>();
        Instantiate(currentCreature.gameObject, currentCreature.startPosition, currentCreature.transform.rotation);

        string randomName = creatures[0].validNames[Random.Range(0, creatures[0].validNames.Count)];
        string randomFood = creatures[0].validFoods[Random.Range(0, creatures[0].validFoods.Count)];
        currentCreature.AssignAnswers(false, randomName, creatures[0].creatureType, randomFood);
    }

    // Update is called once per frame
    void Update()
    {
        
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
