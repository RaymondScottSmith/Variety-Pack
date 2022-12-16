using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_Keypad : TA_Item
{

    public TA_Room extraRoomToAdd;
    public bool leftKeypad;

    public int exitToAdd = 3;

    [TextArea] public string newTextForRoom;
    public override bool UseItem()
    {
        if (TA_Manager.Instance.hasCode)
        {
            oneUse = true;
            TA_Manager.Instance.LogStringWithReturn("You enter the code you memorized from the note that kind crewmember left you.");

            if (leftKeypad)
            {
                TA_Manager.Instance.currentRoom.exits[exitToAdd] = extraRoomToAdd;
                TA_Manager.Instance.currentRoom.roomDescriptions[0] = newTextForRoom;
            }
                
        }
        else
        {
            TA_Manager.Instance.LogStringWithReturn("Unfortunately you don't have the time to try out every number combination. Unless you find the code somewhere, don't bother.");
            
        }

        return true;
    }
}
