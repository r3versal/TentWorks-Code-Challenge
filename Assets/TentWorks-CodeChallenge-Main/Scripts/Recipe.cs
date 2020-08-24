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
        ingredients = new string[3];
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
        for (int i = 0; i < 3; i++)
        {
            string j = vegetables[Random.Range(0, vegetables.Length)];
            ingredients[i] = j;
        }
        return ingredients;
    }
}
