using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_Observ : TA_Room
{ 
    public override void TryToGo(string direction)
    {
        switch (direction)
        {
            case "west":
                TA_Manager.Instance.LogStringWithReturn(westExitDesc);
                TA_Manager.Instance.DisplayLoggedText();
                StartCoroutine(UseExit(exits[3]));
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
