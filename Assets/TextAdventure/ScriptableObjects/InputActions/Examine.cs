using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/InputActions/Examine")]
public class Examine : InputAction
{
    public override void RespondToInput(TextGameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords[0] == "look" && separatedInputWords[1] == "around" && separatedInputWords.Length == 2)
        {
            controller.DisplayRoomText();
            return;
        }
        controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun
            (controller.interactableItems.examineDictionary, 
                separatedInputWords[0], separatedInputWords[1]));
    }
}
