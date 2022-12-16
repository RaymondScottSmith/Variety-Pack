using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_Key : TA_Item
{
    public TA_Room useRoom;
    public TA_Desk useDesk;
    public TA_Item newItem;

    [TextArea] public string newDeskDescription;
    [TextArea] public string useDescription = "You open the desk drawer with the key.";
    public override bool UseItem()
    {
        if (TA_Manager.Instance.currentRoom == useRoom)
        {
            TA_Manager.Instance.LogStringWithReturn(useDescription);
            useRoom.itemsInRoom.Add(newItem);
            useDesk.examineDescription = newDeskDescription;
            TA_Manager.Instance.LogStringWithReturn(newItem.passiveDescription);
            return true;
        }

        return false;
    }
}
