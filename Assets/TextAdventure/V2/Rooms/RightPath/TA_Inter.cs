using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_Inter : TA_Room
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
}
