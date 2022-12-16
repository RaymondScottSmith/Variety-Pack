using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_Holding : TA_Room
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
                if (exits[1] != null)
                {
                    TA_Manager.Instance.LogStringWithReturn(eastExitDesc);
                    TA_Manager.Instance.DisplayLoggedText();

                    StartCoroutine(UseExit(exits[1]));
                }
                else
                {
                    CantGoThere(direction);
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
