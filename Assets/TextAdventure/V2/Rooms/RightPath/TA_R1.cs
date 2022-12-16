using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_R1 : TA_Room
{
    public override void TryToGo(string direction)
    {
        TA_Manager.Instance.CheckPast(false);
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
