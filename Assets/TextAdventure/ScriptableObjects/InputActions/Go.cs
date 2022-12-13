using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Go_Action", menuName = "ScriptableObjects/InputActions/Go")]
public class Go : InputAction
{
    public override void RespondToInput(TextGameController controller, string[] separatedInputWords)
    {
        //Send second word because it's the operative word for the statement
        controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);
    }
}
