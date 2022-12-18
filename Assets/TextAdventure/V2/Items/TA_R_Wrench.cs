using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_R_Wrench : TA_Item
{
    public TA_Room useRoom;
    public TA_R_Door door;
    public TA_R_Hall1 hall;
    [TextArea] public string newHallDescription;

    [TextArea] public string newDoorDescription;
    public override bool UseItem()
    {
        if (TA_Manager.Instance.currentRoom == useRoom)
        {
            TA_Manager.Instance.LogStringWithReturn("With a few quick turns of the wrench you get the door revolving again.");
            TA_Manager.Instance.soundManager.PlayClip(5);
            door.isFixed = true;
            useRoom.roomDescriptions[0] = newHallDescription;
            door.examineDescription = newDoorDescription;
            hall.doorOpened = true;
            door.visible = false;
            return true;
        }

        return false;
    }
}
