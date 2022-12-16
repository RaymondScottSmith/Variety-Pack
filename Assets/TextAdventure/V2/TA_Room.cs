using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TA_Room : MonoBehaviour
{
    public TA_Room[] exits;

    public List<TA_Item> itemsInRoom;
    public int descriptionToShow = 0;

    public float delayToTransition = 1f;

    [TextArea]
    public string firstDescription;

    public List<string> roomDescriptions;
    
    [TextArea] public string westExitDesc, eastExitDesc, northExitDesc, southExitDesc;

    
    
    
    void Awake()
    {
        roomDescriptions.Add(firstDescription);
        
    }

    public abstract void TryToGo(string direction);

    public void CantGoThere(string direction)
    {
        TA_Manager.Instance.LogStringWithReturn("Can't go " + direction + ".");
    }

    public void ListAllItemsInRoom()
    {
        foreach (TA_Item item in itemsInRoom)
        {
            if (item.visible)
                TA_Manager.Instance.LogStringWithReturn(item.passiveDescription);
        }
    }

    public void RemoveItemFromRoom(string item)
    {
        foreach (TA_Item roomItem in itemsInRoom)
        {
            if (roomItem.keyword == item)
            {
                itemsInRoom.Remove(roomItem);
                return;
            }
        }
    }

    public IEnumerator UseExit(TA_Room newRoom)
    {
        yield return new WaitForSeconds(delayToTransition);
        TA_Manager.Instance.GoToNewRoom(newRoom);
    }

}
