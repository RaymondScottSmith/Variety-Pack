using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_CodesR : TA_Item
{
    public TA_LineUp useRoom;
    public TA_Window window;
    public override bool UseItem()
    {
        if (TA_Manager.Instance.currentRoom == useRoom)
        {
            useRoom.PutCodeOnWindow();
            window.ChangeWindow();
            PlayerPrefs.SetInt("TextRPaper",1);
            return true;
        }

        return false;
    }
}
