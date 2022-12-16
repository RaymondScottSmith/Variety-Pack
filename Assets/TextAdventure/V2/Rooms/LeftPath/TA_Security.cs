using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_Security : TA_Room
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
                if (exits[3] != null)
                {
                    TA_Manager.Instance.LogStringWithReturn(westExitDesc);
                    TA_Manager.Instance.DisplayLoggedText();

                    StartCoroutine(UseExit(exits[3]));
                }
                else
                {
                    CantGoThere(direction);
                }
                break;
            
            default:
                CantGoThere(direction);
                break;
        }
    }
}
