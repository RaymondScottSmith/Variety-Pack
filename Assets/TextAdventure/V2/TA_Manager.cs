using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TA_Manager : MonoBehaviour
{
    [Header("Help Response")] [TextArea] public string helpResponse;
    [Header("ResetButton")] public bool resetProgress;
    public TA_Observ observerRoom;
    public TA_LineUp lineupRoom;
    public TA_Item presetRNote;
    public TA_Item presetLNote;

    [Header("UI")]
    public TMP_Text displayText;
    public TMP_InputField inputField;

    [Header("Rooms")]
    public TA_Room startingRoom;
    public TA_Room currentRoom;

    [Header("Formatting")] public List<string> wordsToIgnore;
    public List<string> inputActions;
    
    private List<string> actionLog = new List<string>();

    public TA_SoundManager soundManager;

    public bool hasCode;

    public List<TA_Item> inventory;


    public static TA_Manager Instance;

    public bool wearingSuit;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        soundManager = GetComponent<TA_SoundManager>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    void Start()
    {
        GoToStartRoom();
    }

    public void CheckPast(bool isOnLeft)
    {
        bool placedPaperOnLeftSide = PlayerPrefs.GetInt("TextLPaper") == 1;
        bool placedPaperOnRightSide = PlayerPrefs.GetInt("TextRPaper") == 1;
        if (isOnLeft)
        {
            PlayerPrefs.SetInt("TextLPaper",0);
            if (placedPaperOnRightSide)
            {
                observerRoom.itemsInRoom.Add(presetLNote);
            }
        }
        else
        {
            PlayerPrefs.SetInt("TextRPaper",0);
            if (placedPaperOnLeftSide)
            {
                lineupRoom.itemsInRoom.Add(presetRNote);
            }
        }
    }

    public void WinGame()
    {
        LogStringWithReturn("Congratulations, you made it to the escape bay!");
        LogStringWithReturn("Type restart to play again or menu to return to menu.");
    }
    
    void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        LogStringWithReturn(userInput);

        foreach (string word in wordsToIgnore)
        {
            userInput = userInput.Replace(" " + word + " ", " ");
        }
        
        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);

        if (CheckActionValidity(separatedInputWords[0]))
        {

            if (separatedInputWords.Length > 1)
            {
                switch (separatedInputWords[0])
                {
                    case "go":
                        HandleGoCommand(separatedInputWords[1]);
                        break;
                    case "look":
                        HandleLookCommand(separatedInputWords[1]);
                        break;
                    case "examine":
                        HandleLookCommand(separatedInputWords[1]);
                        break;
                    case "take":
                        HandleTakeCommand(separatedInputWords[1]);
                        break;
                    case "grab":
                        HandleTakeCommand(separatedInputWords[1]);
                        break;
                    case "use":
                        HandleUseCommand(separatedInputWords[1]);
                        break;
                    case "help":
                        HandleHelpRequest(separatedInputWords[1]);
                        break;
                    default:
                        LogStringWithReturn("I don't recognize " + separatedInputWords[0] + ".");
                        break;
                }
                
            }
            else
            {
                switch (separatedInputWords[0])
                {
                    case "restart":
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        break;
                    case "menu":
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        break;
                    case "help":
                        HandleHelpRequest();
                        break;
                    case "inventory":
                        ListInventory();
                        break;
                    default:
                        LogStringWithReturn("You want to " + separatedInputWords[0] + " what?");
                        break;
                }
                
            }
            
            
        }
        else
        {
            LogStringWithReturn("I don't recognize " + separatedInputWords[0] + ".");
        }

        InputComplete();
    }

    private void HandleHelpRequest(string separatedInputWord = null)
    {
        LogStringWithReturn(helpResponse);
    }

    public void HandleUseCommand(string whatToUse)
    {
        foreach(TA_Item item in inventory)
        {
            if (item.keyword == whatToUse)
            {
                if (item.canUse)
                {
                    if (!item.UseItem())
                    {
                        LogStringWithReturn("Can't use " + item.keyword + " here.");
                        return;
                    }
                    else if (item.oneUse)
                    {
                        inventory.Remove(item);
                        return;
                    }
                }

                return;
            }
        }

        foreach (TA_Item item in currentRoom.itemsInRoom)
        {
            if (item.keyword == whatToUse)
            {
                if (item.canUse)
                {
                    if (!item.UseItem())
                    {
                        LogStringWithReturn("Can't use " + item.keyword + ".");
                        return;
                    }
                    else if (item.oneUse)
                    {
                        currentRoom.itemsInRoom.Remove(item);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    LogStringWithReturn("Can't use " + item.keyword + ".");
                    return;
                }
            }
        }
        
        LogStringWithReturn("I don't recognize " + whatToUse + ".");
    }

    public void HandleTakeCommand(string whatToTake)
    {
        foreach (TA_Item item in currentRoom.itemsInRoom)
        {
            if (item.keyword == whatToTake)
            {
                if (item.canCarry)
                {
                    LogStringWithReturn(item.takeDescription);
                    inventory.Add(item);
                    currentRoom.RemoveItemFromRoom(whatToTake);
                    return;
                }
                else
                {
                    LogStringWithReturn("You can't carry " + whatToTake + ".");
                    return;
                }
            }
        }
        LogStringWithReturn("I don't recognize " + whatToTake + ".");
        return;
    }

    public void ListInventory()
    {
        LogStringWithReturn("In your inventory you have: ");
        foreach (TA_Item item in inventory)
        {
            LogStringWithReturn(item.keyword);
        }
    }

    public void DescribeRoom()
    {
        LogStringWithReturn(currentRoom.roomDescriptions[currentRoom.descriptionToShow]);
        currentRoom.ListAllItemsInRoom();
    }

    public void HandleLookCommand(string whatToLook)
    {
        if (whatToLook == "around")
        {
            DescribeRoom();
        }
        else if (whatToLook == "inventory")
        {
            ListInventory();
        }
        else
        {
            foreach (TA_Item item in currentRoom.itemsInRoom)
            {
                if (item.keyword == whatToLook)
                {
                    LogStringWithReturn(item.examineDescription);
                    if (item.keyword == "note")
                    {
                        hasCode = true;
                    }
                    return;
                }
            }

            foreach (TA_Item item in inventory)
            {
                if (item.keyword == whatToLook)
                {
                    LogStringWithReturn(item.examineDescription);
                    return;
                }
            }
            
            LogStringWithReturn("Can't look at " + whatToLook + ".");
        }
    }

    public void HandleGoCommand(string whereToGo)
    {
        switch (whereToGo)
        {
            case "north":
                break;
            case "south":
                break;
            case "west":
                break;
            case "east":
                break;
            default:
                LogStringWithReturn("I don't recognize " + whereToGo + ".");
                return;
                break;
                
        }

        currentRoom.TryToGo(whereToGo);
    }

    private bool CheckActionValidity(string action)
    {
        foreach(string input in inputActions)
        {
            if (action == input)
            {
                return true;
            }
        }

        return false;
    }

    void InputComplete()
    {
        DisplayLoggedText();
        //Reactivate field after hitting enter
        inputField.ActivateInputField();
        inputField.text = null;
    }
    
    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsText;
    }
    
    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    public void GoToStartRoom()
    {
        currentRoom = startingRoom;
        LogStringWithReturn(startingRoom.roomDescriptions[startingRoom.descriptionToShow]);
        DisplayLoggedText();
    }

    public void GoToNewRoom(TA_Room newRoom)
    {
        currentRoom = newRoom;
        DescribeRoom();
        DisplayLoggedText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
