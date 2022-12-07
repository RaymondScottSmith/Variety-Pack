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
    public List<string> dolphinNames;

    [SerializeField] private List<string> allWaterTypes;
    [SerializeField] private List<string> allOrigins;
    [SerializeField] private List<string> allTeams;
    [SerializeField] private List<string> allFood;

    [SerializeField] private GameObject canvas;

    string species = "";
    string food = "";
    string water = "";
    
    string randName = "";
    string origin = "";
    string team = "";

    [SerializeField] private AudioClip longBubbleSound;

    [SerializeField] private List<AudioClip> shortBubbleSounds;

    private AudioSource myAudioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }

    public void HideCanvas()
    {
        canvas.gameObject.SetActive(false);
    }

    public void ShowCanvas()
    {
        canvas.gameObject.SetActive(true);
    }

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        HideCanvas();
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
            int lyingTrait = Random.Range(0, 6);
            switch (lyingTrait)
            {
                case 0:
                    List<string> names = new List<string>();
                    names.AddRange(dolphinNames);
                    foreach (string oldName in newCreature.validNames)
                    {
                        names.Remove(oldName);
                    }
                    randName = names[Random.Range(0, names.Count)];
                    break;
                case 1:
                    List<string> newFoods = new List<string>();
                    newFoods.AddRange(allFood);
                    foreach (string oldFood in newCreature.validFoods)
                    {
                        newFoods.Remove(oldFood);
                    }
                    food = newFoods[Random.Range(0, newFoods.Count)];
                    break;
                case 2:
                    CreatureType newType = new CreatureType();
                    newType = newCreature.creatureType;
                    int eExit = 0;
                    while (newType == newCreature.creatureType)
                    {
                        newType = (CreatureType)Random.Range(0, 7);
                        eExit++;
                        if (eExit > 10)
                            break;
                    }
                    species = GetSpeciesName(newType);
                    break;
                case 3:
                    List<string> newWaters = new List<string>();
                    newWaters.AddRange(allWaterTypes);
                    newWaters.Remove(water);
                    water = newWaters[Random.Range(0, newWaters.Count)];
                    break;
                case 4:
                    List<string> newOrigin = new List<string>();
                    newOrigin.AddRange(allOrigins);
                    newOrigin.Remove(origin);
                    origin = newOrigin[Random.Range(0, newOrigin.Count)];
                    break;
                case 5:
                    List<string> newTeam = new List<string>();
                    newTeam.AddRange(allTeams);
                    newTeam.Remove(team);
                    team = newTeam[Random.Range(0, newTeam.Count)];
                    break;
            }
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
        myAudioSource.PlayOneShot(longBubbleSound);
        
        ContinueStory();
    }
    

    public void MakeChoice(int choiceNumber)
    {
        currentStory.ChooseChoiceIndex(choiceNumber);
        
        myAudioSource.PlayOneShot(shortBubbleSounds[Random.Range(0, shortBubbleSounds.Count)]);
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

    public void ExitDialogueMode()
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
