using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log(recipeSize);
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
