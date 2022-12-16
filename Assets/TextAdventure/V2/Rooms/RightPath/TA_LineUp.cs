using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_LineUp : TA_Room
{
    public override void TryToGo(string direction)
    {
        switch (direction)
        {
            case "east":
                TA_Manager.Instance.LogStringWithReturn(eastExitDesc);
                TA_Manager.Instance.DisplayLoggedText();
                StartCoroutine(UseExit(exits[1]));
                break;
            default:
                CantGoThere(direction);
                break;
        }
    }
    
    public void PutCodeOnWindow()
    {
        TA_Manager.Instance.LogStringWithReturn("You place the paper against the window. The crewmember notices, reads the paper, and gives you a thumbs up.");
    }
}
