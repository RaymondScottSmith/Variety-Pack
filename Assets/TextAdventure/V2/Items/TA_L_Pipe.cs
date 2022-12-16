using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_L_Pipe : TA_Item
{
    public TA_Room useRoom;
    public TA_LWheel wheel;
    public override bool UseItem()
    {
        if (TA_Manager.Instance.currentRoom == useRoom)
        {
            TA_Manager.Instance.LogStringWithReturn("You replace the damaged pipe section with this new one.");
            wheel.pipeInPlace = true;
            return true;
        }

        return false;
    }
}
