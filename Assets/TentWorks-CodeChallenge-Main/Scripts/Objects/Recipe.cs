///-----------------------------------------------------------------
///   Class:          Recipe
///   Description:    This script handles the possible recipe combinations and is called by the Customer class when needed
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
#region using directives
using UnityEngine;
#endregion

public class Recipe : MonoBehaviour
{
    private string[] vegetables;
    public string[] ingredients;

    void Awake()
    {
        vegetables = new string[]
            {
                 "Lettuce",
                 "Carrot",
                 "Onion",
                 "RedBellPepper",
                 "YellowBellPepper",
                 "Tomato"
            };
    }

    public string[] GetVegetables()
    {
        return vegetables;
    }

    public string[] GetRecipe()
    {
        int recipeSize = Random.Range(2,4);
        ingredients = new string[recipeSize];
        if(vegetables == null)
        {
            vegetables = new string[]
            {
                 "Lettuce",
                 "Carrot",
                 "Onion",
                 "RedBellPepper",
                 "YellowBellPepper",
                 "Tomato"    };
        }
        for (int i = 0; i < recipeSize; i++)
        {
            string j = vegetables[Random.Range(0, vegetables.Length)];
            ingredients[i] = j;
        }
        return ingredients;
    }
}
