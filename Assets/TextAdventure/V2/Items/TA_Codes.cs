using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_Codes : TA_Item
{
    public TA_Observ useRoom;
    public TA_Window window;
    public override bool UseItem()
    {
        if (TA_Manager.Instance.currentRoom == useRoom)
        {
            useRoom.PutCodeOnWindow();
            window.ChangeWindow();
            PlayerPrefs.SetInt("TextLPaper",1);
            return true;
        }

        return false;
    }
}
