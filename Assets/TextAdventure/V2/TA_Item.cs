using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TA_Item : MonoBehaviour
{
    public bool canCarry;
    public bool oneUse;
    public bool visible;
    public bool canUse;
    public string keyword;

    [TextArea] public string passiveDescription;
    [TextArea] public string examineDescription;
    [TextArea] public string takeDescription;

    public abstract bool UseItem();

}
