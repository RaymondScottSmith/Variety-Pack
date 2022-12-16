using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/InputActions/Use")]
public class Use : InputAction
{
    public override void RespondToInput(TextGameController controller, string[] separatedInputWords)
    {
        


        if (controller.interactableItems.UseItem(separatedInputWords))
        {
            /*
            //Need to put an if statement here to make sure that we only do this when we should be able to
            controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun
            (controller.interactableItems.useRespDictionary, 
                separatedInputWords[0], separatedInputWords[1]));
                */
            
            controller.interactableItems.RemoveFromUseRespDictionary(keyWord);
        };
        
    }
}
