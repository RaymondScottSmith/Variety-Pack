using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "TextAdventure/TA_Exit")]
public class TA_Exit : ScriptableObject
{
    public TA_Room room1;
    public TA_Room room2;
    
    public string keyString;
    [TextArea]
    public string exitDescription;
}
