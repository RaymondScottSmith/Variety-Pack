using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ActionResponse/ChangeRoomResponse")]
public class ChangeRoomResponse : ActionResponse
{
    public Room roomToChangeTo;
    public override bool DoActionResponse(TextGameController controller, string keyword = null)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            if (keyword != null)
            {
                controller.LogStringWithReturn(controller.interactableItems.useRespDictionary[keyword]);
            }
            
            /*
            foreach (Exit exit in controller.roomNavigation.currentRoom.exits)
            {
                foreach (Exit oppExit in exit.valueRoom.exits)
                {
                    if (oppExit.valueRoom == controller.roomNavigation.currentRoom)
                    {
                        oppExit.valueRoom = roomToChangeTo;
                    }
                }
            }
            */
            controller.roomNavigation.currentRoom = roomToChangeTo;
            controller.DisplayRoomText();
            
            return true;
        }

        return false;
    }
    
    
}
