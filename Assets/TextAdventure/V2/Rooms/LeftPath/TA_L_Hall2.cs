using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_L_Hall2 : TA_Room
{

    public bool fireGone;
    public override void TryToGo(string direction)
    {
        switch (direction)
        {
            case "north":
                if (!fireGone)
                {
                    TA_Manager.Instance.LogStringWithReturn("...and walk into the flames? What are you crazy?");
                    TA_Manager.Instance.DisplayLoggedText();
                }
                else
                {
                    TA_Manager.Instance.LogStringWithReturn(northExitDesc);
                    TA_Manager.Instance.DisplayLoggedText();
                    StartCoroutine(UseExit(exits[0]));
                }
                
                break;
            case "east":
                TA_Manager.Instance.LogStringWithReturn(eastExitDesc);
                TA_Manager.Instance.DisplayLoggedText();

                StartCoroutine(UseExit(exits[1]));
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
