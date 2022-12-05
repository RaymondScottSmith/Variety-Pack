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

    private TextMeshProUGUI[] choicesText;

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
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        EnterDialogueMode(sampleDialogue);
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        
        dialogueIsPlaying = true;
        currentStory = new Story(inkJSON.text);
        ContinueStory();
        
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
