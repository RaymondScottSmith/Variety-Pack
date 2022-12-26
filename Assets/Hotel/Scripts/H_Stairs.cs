using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Stairs : H_Interactive
{
    
    public override void Interact()
    {
        if (H_GameManager.Instance.zombiesInScene > 0)
        {
            H_GameManager.Instance.UpdateInstructions("Destroy All Zombies First");
        }
        else
        {
            H_GameManager.Instance.WinGame();
        }
    }
}
