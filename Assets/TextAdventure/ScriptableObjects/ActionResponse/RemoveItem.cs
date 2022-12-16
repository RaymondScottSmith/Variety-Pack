using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ActionResponse/RemoveItem")]
public class RemoveItem : ActionResponse
{
    public string itemToRemove;
    public override bool DoActionResponse(TextGameController controller, string keyword = null)
    {
        if (controller.interactableItems.NounsInInventory().Contains(itemToRemove))
        {
            controller.RemoveFromInventory(itemToRemove);
            return true;
        }

        return false;
    }
}
