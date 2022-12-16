using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/InputActions/Inventory")]
public class Inventory : InputAction
{
    public override void RespondToInput(TextGameController controller, string[] separatedInputWords)
    {
        controller.interactableItems.DisplayInventory();
    }
}
