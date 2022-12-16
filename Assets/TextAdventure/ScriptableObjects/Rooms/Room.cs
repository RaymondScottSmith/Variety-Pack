using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Side_RoomName", menuName = "ScriptableObjects/Room")]
public class Room : ScriptableObject
{
    [TextArea]
    public string description;
    public string roomName;

    public Exit[] exits;
    public InteractableObject[] interObjectsInRoom;

    
}
