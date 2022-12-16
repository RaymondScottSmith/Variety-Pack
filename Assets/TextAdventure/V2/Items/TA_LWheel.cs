using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class TA_LWheel : TA_Item
{
    public int currentPressure = 0;
    public bool pipeInPlace;
    private bool displayOn;

    public TA_Item display;
    public override bool UseItem()
    {
        
        if (!pipeInPlace)
        {
            TA_Manager.Instance.LogStringWithReturn("You turn the wheel. There's a short alarm sound and an automated message says: 'Please replace damaged pipe before adding pressure.'");
            return true;
        }

        currentPressure += 30;
        TA_Manager.Instance.LogStringWithReturn("You turn the wheel.");
        if (!displayOn)
        {
            displayOn = true;
            TA_Manager.Instance.LogStringWithReturn("The display above the wheel lights up.");
        }

        
        if (currentPressure > 80)
        {
            currentPressure = 0;
            TA_Manager.Instance.LogStringWithReturn("There's a short alarm sound and an automated message says: 'System cannot handle pressure greater than 80 PSI. Resetting to 0 pressure.'");
            display.examineDescription = "The display shows: " + currentPressure + " PSI.";
            return true;
        }
        if (currentPressure == 60)
        {
            TA_Manager.Instance.LogStringWithReturn("A small light above the wheel turns green.");
        }
        display.examineDescription = "The display shows: " + currentPressure + " PSI.";

        
        return true;
    }
}
