using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class TA_Start : TA_Room
{
    
    // Start is called before the first frame update
    /*
    void Awake()
    {
        roomDescriptions.Add(firstDescription);
        
    }
    */

    
    public override void TryToGo(string direction)
    {
        switch (direction)
        {
            case "east":
                TA_Manager.Instance.LogStringWithReturn(eastExitDesc);
                TA_Manager.Instance.DisplayLoggedText();

                StartCoroutine(UseExit(exits[1]));
                break;
            case "west":
                TA_Manager.Instance.LogStringWithReturn(westExitDesc);
                TA_Manager.Instance.DisplayLoggedText();
                StartCoroutine(UseExit(exits[0]));
                break;
            default:
                CantGoThere(direction);
                break;
        }
    }
}
