using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Sandwich", menuName = "ScriptableObjects/Sandwich")]
public class Sandwich : ScriptableObject
{
    public string name;
    public List<Sa_Ingredient.Ingredient> ingredients;
    public int cost;
}
