using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sa_Ingredient : MonoBehaviour
{
    public Ingredient ingType = Ingredient.Default;
    public enum Ingredient
    {
        Bread,
        Toast,
        Tuna,
        Salmon,
        Anchov,
        A_Cheese,
        S_Cheese,
        Dish,
        Tomato,
        Lettuce,
        Bacon,
        Ham,
        Default
    }
}
