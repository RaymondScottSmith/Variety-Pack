using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/InputActions/Take")]
public class Take : InputAction
{
    public override void RespondToInput(TextGameController controller, string[] separatedInputWords)
    {
        Dictionary<string, string> takeDictionary = controller.interactableItems.Take(separatedInputWords);

        if (takeDictionary != null)
        {
            controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun
                (takeDictionary, separatedInputWords[0], 
                    separatedInputWords[1]));
        }
    }
}
