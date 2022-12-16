using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_L_Button : TA_Item
{

    public TA_LWheel wheel;
    public TA_L_Hall2 hall;

    [TextArea] public string newHallDescription;
    public override bool UseItem()
    {
        if (wheel.currentPressure == 60)
        {
            TA_Manager.Instance.LogStringWithReturn("You hear a whooshing sound as the fire suppression system starts up.");
            TA_Manager.Instance.currentRoom.RemoveItemFromRoom("wheel");
            TA_Manager.Instance.currentRoom.RemoveItemFromRoom("button");
            hall.roomDescriptions[0] = newHallDescription;
            hall.fireGone = true;
            return true;
        }
        else
        {
            TA_Manager.Instance.LogStringWithReturn("You hear a buzzing sound and an automated message saying: 'Please ensure the proper PSI is in the system before reset.'");
            return true;
        }
    }
}
