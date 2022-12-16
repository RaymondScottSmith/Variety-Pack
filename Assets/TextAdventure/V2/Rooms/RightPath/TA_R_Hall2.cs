using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_R_Hall2 : TA_Room
{
    [TextArea]
    public string failureMessage;
    public override void TryToGo(string direction)
    {
        switch (direction)
        {
            case "north":
                if (TA_Manager.Instance.wearingSuit)
                {
                    TA_Manager.Instance.LogStringWithReturn(northExitDesc);
                    TA_Manager.Instance.DisplayLoggedText();
                    //StartCoroutine(UseExit(exits[2]));
                    TA_Manager.Instance.WinGame();
                }
                else
                {
                    TA_Manager.Instance.LogStringWithReturn(failureMessage);
                    if (PlayerPrefs.GetInt("TextLPaper") == 1)
                    {
                        TA_Manager.Instance.LogStringWithReturn("Try to find a radiation suit.");
                    }
                    else
                    {
                        TA_Manager.Instance.LogStringWithReturn("You have a sinking feeling that finding a suit now is impossible. Maybe if you had chosen differently, or someone else had helped a stranger.");
                        TA_Manager.Instance.LogStringWithReturn("Type restart to try again.");
                    }
                }
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
