using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TA_Suit : TA_Item
{
    public override bool UseItem()
    {
        TA_Manager.Instance.LogStringWithReturn("You put on the suit.");
        TA_Manager.Instance.wearingSuit = true;
        return true;
    }
}
