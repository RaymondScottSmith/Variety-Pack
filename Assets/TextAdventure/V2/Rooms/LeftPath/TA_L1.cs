using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_L1 : TA_Room
{
    private bool firstTime = true;
    public override void TryToGo(string direction)
    {
        if (firstTime)
        {
            TA_Manager.Instance.CheckPast(true);
            firstTime = false;
        }
        
        switch (direction)
        {
            case "north":
                TA_Manager.Instance.LogStringWithReturn(northExitDesc);
                TA_Manager.Instance.DisplayLoggedText();
                StartCoroutine(UseExit(exits[0]));
                break;
            default:
                CantGoThere(direction);
                break;
        }
    }
}
