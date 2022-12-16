using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_R_Hall1 : TA_Room
{

    public bool doorOpened;
    public override void TryToGo(string direction)
    {
        switch (direction)
        {
            case "north":
                if (!doorOpened)
                {
                    TA_Manager.Instance.LogStringWithReturn("You could maybe fit a single arm through that gap. Not passing now.");
                    TA_Manager.Instance.DisplayLoggedText();
                }
                else
                {
                    TA_Manager.Instance.LogStringWithReturn(northExitDesc);
                    TA_Manager.Instance.DisplayLoggedText();
                    StartCoroutine(UseExit(exits[0]));
                }
                
                break;
            case "south":
                TA_Manager.Instance.LogStringWithReturn(southExitDesc);
                TA_Manager.Instance.DisplayLoggedText();

                StartCoroutine(UseExit(exits[2]));
                break;
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
