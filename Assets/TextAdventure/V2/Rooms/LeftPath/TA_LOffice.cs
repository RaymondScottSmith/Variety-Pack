using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_LOffice : TA_Room
{
    public override void TryToGo(string direction)
    {
        switch (direction)
        {
            case "north":
                TA_Manager.Instance.LogStringWithReturn(northExitDesc);
                TA_Manager.Instance.DisplayLoggedText();
                StartCoroutine(UseExit(exits[0]));
                break;
            case "south":
                TA_Manager.Instance.LogStringWithReturn(southExitDesc);
                TA_Manager.Instance.DisplayLoggedText();
                StartCoroutine(UseExit(exits[2]));
                break;
            default:
                CantGoThere(direction);
                break;
        }
    }
}
