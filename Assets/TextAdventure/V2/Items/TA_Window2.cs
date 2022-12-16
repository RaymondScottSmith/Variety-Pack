using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TA_Window2 : TA_Item
{
    [TextArea] public string changedDescription;
    [TextArea] public string changedPassive;

    public override bool UseItem()
    {
        throw new System.NotImplementedException();
    }

    public void ChangeWindow()
    {
        passiveDescription = changedPassive;
        examineDescription = changedDescription;
    }
}
