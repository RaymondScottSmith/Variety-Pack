using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    public List<InteractableObject> useableItemList;
    
    public Dictionary<string, string> examineDictionary = new Dictionary<string,string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string,string>();
    public Dictionary<string, string> useRespDictionary = new Dictionary<string, string>();

    private Dictionary<string, ActionResponse[]> useDictionary = new Dictionary<string, ActionResponse[]>();
    public List<string> nounsInRoom = new List<string>();

    private List<string> nounsInInventory = new List<string>();

    private TextGameController controller;

    void Awake()
    {
        controller = GetComponent<TextGameController>();
    }

    public List<string> NounsInInventory()
    {
        return nounsInInventory;
    }
    
    public string GetObjectsNotInInventory(Room currentRoom, int i)
    {
        InteractableObject interactableInRoom = currentRoom.interObjectsInRoom[i];
        if (!nounsInInventory.Contains(interactableInRoom.noun))
        {
            nounsInRoom.Add(interactableInRoom.noun);
            return interactableInRoom.description;
        }

        return null;
    }
    
    

    public void AddActionResponsesToUseDictionary()
    {
        foreach (string item in nounsInInventory)
        {
            InteractableObject interactableObjectInInventory = GetInteractableObjectFromUseableList(item);
            if (interactableObjectInInventory == null)
            {
                continue;
            }
            for(int i = 0; i < interactableObjectInInventory.interactions.Length; i++)
            {
                /*
                Interaction interaction = interactableObjectInInventory.interactions[i];
                if (interaction.actionResponse == null)
                {
                    continue;
                }

                if (!useDictionary.ContainsKey(item))
                {
                    useDictionary.Add(item, interaction.actionResponse);
                }
                */
                
                Interaction interaction = interactableObjectInInventory.interactions[i];
                if (interaction.actionResponses.Count() == 0)
                {
                    continue;
                }

                if (!useDictionary.ContainsKey(item))
                {
                    /*
                    foreach(ActionResponse resp in interaction.actionResponses)
                        useDictionary.Add(item, resp);
                        */
                    useDictionary.Add(item, interaction.actionResponses);
                }
            }
        }
    }

    private InteractableObject GetInteractableObjectFromUseableList(string noun)
    {
        foreach (InteractableObject useObj in useableItemList)
        {
            if (useObj.noun == noun)
            {
                return useObj;
            }
        }

        return null;
    }

    public void DisplayInventory()
    {
        controller.LogStringWithReturn("You look in your backpack, inside you have: ");
        foreach (string item in nounsInInventory)
        {
            controller.LogStringWithReturn(item);
        }
    }

    public void RemoveFromInventory(string toBeRemoved)
    {
        nounsInInventory.Remove(toBeRemoved);
    }

    public void ClearCollections()
    {
        examineDictionary.Clear();
        takeDictionary.Clear();
        //useRespDictionary.Clear();
        nounsInRoom.Clear();
    }

    public void RemoveFromUseRespDictionary(string key)
    {
        useRespDictionary.Remove(key);
    }

    public Dictionary<string, string> Take(string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];
        if (nounsInRoom.Contains(noun))
        {
            nounsInInventory.Add(noun);
            AddActionResponsesToUseDictionary();
            nounsInRoom.Remove(noun);
            return takeDictionary;
        }
        else
        {
            controller.LogStringWithReturn("There is no " + noun + " here to take.");
            return null;
        }
        
    }
    public bool UseItem(string[] separatedInputWords)
    {
        string nountToUse = separatedInputWords[1];
        if (nounsInInventory.Contains(nountToUse))
        {
            if (useDictionary.ContainsKey(nountToUse))
            {
                for (int i = 0; i < useDictionary[nountToUse].Count(); i++)
                {
                    bool actionResult = useDictionary[nountToUse][i].DoActionResponse(controller, separatedInputWords[1]);
                    if (!actionResult)
                    {
                        controller.LogStringWithReturn("Hmm... Nothing happens.");
                        break;
                    }
                    else
                    {
                        return true;
                    }
                }

                //return false;
            }
            else
            {
                Debug.Log("You can't use the line");
                controller.LogStringWithReturn("You can't use the " + nountToUse);
                return false;
            }
        }
        else
        {
            controller.LogStringWithReturn("There is no " + nountToUse + " in your inventory.");
            return false;
        }

        return false;
    }
}
