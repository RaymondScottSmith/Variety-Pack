using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")] [SerializeField]
    private TMP_Text dialogueText;

    private Story currentStory;

    public static DialogueManager Instance;

    private bool dialogueIsPlaying;

    [SerializeField] private TextAsset sampleDialogue;

    [Header("Choices UI")] [SerializeField]
    private GameObject[] choices;

    private TMP_Text[] choicesText;

    [Header("Creature Info")] [SerializeField]
    private CreatureInfo whaleInfo;

    [SerializeField] private List<string> intros;
    [SerializeField] private List<string> whaleNames;
    [SerializeField] private List<string> names;

    [SerializeField] private List<string> allWaterTypes;
    [SerializeField] private List<string> allOrigins;
    [SerializeField] private List<string> allTeams;
    [SerializeField] private List<string> allFood;
    
    string species = "";
    string food = "";
    string water = "";
    
    string randName = "";
    string origin = "";
    string team = "";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }

    void Start()
    {
        names.AddRange(whaleNames);
        int index = 0;
        choicesText = new TMP_Text[choices.Length];
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TMP_Text>();
            index++;
        }
        //EnterDialogueMode(sampleDialogue);
    }

    public void EnterDialogueMode(CreatureInfo newCreature, bool isLying)
    {
       
        
        
        
        /*
        if (newCreature.creatureType == CreatureType.Whale)
            randName = whaleNames[Random.Range(0, whaleNames.Count)];
        else
        {
            randName = names[Random.Range(0, names.Count)];
        }
        */

        dialogueText.text = "";
        randName = newCreature.validNames[Random.Range(0, newCreature.validNames.Count)];
        species = GetSpeciesName(newCreature.creatureType);
        food = newCreature.validFoods[Random.Range(0, newCreature.validFoods.Count)];
        water = newCreature.waterType;
        origin = newCreature.origin;
        team = newCreature.sportTeam;
        if (water == "")
        {
            water = allWaterTypes[Random.Range(0, allWaterTypes.Count)];
        }

        if (origin == "")
        {
            origin = allOrigins[Random.Range(0, allOrigins.Count)];
        }

        if (team == "")
        {
            team = allTeams[Random.Range(0, allTeams.Count)];
        }
        
        if (isLying)
        {
            water = "rose water";
            food = "babies";
            team = "Miami Dolphins";
        }


        
        
    }

    public void StartDialogue()
    {
        int randIntro = Random.Range(0, intros.Count);
        dialogueIsPlaying = true;
        currentStory = new Story(sampleDialogue.text);
        currentStory.EvaluateFunction("setIntro", intros[randIntro]);
        currentStory.EvaluateFunction("setName", randName);
        currentStory.EvaluateFunction("setSpecies", species);
        currentStory.EvaluateFunction("setFood", food);
        currentStory.EvaluateFunction("setWater", water);
        currentStory.EvaluateFunction("setOrigin", origin);
        currentStory.EvaluateFunction("setTeam", team);
        
        ContinueStory();
    }
    

    public void MakeChoice(int choiceNumber)
    {
        currentStory.ChooseChoiceIndex(choiceNumber);
        ContinueStory();
    }

    private string GetSpeciesName(CreatureType ct)
    {
        string result = "";
        switch (ct)
        {
            case CreatureType.Whale:
                result = "Whale";
                break;
            case CreatureType.Dugong:
                result = "Dugong";
                break;
            case CreatureType.GreatWhite:
                result = "Great White Shark";
                break;
            case CreatureType.Hammerhead:
                result = "Hammerhead Shark";
                break;
            case CreatureType.MantaRay:
                result = "Manta Ray";
                break;
            case CreatureType.SeaTurtle:
                result = "Sea Turtle";
                break;
            case CreatureType.Sunfish:
                result = "Sunfish";
                break;
            case CreatureType.Swordfish:
                result = "Swordfish";
                break;
            case CreatureType.Dolphin:
                result = "Dolphin";
                break;
        }

        return result;
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        
        ContinueStory();
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        //defensive check to make sure our UI can support the number coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        //enable and initialize the choices up to the amount of choices for this line of dialogue

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        //Go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }
}
