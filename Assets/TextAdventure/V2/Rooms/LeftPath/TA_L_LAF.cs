using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_L_LAF : TA_Room
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
}
