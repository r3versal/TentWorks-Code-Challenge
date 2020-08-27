///-----------------------------------------------------------------
///   Class:          Plate
///   Description:    This script is attached to the plate object and handles Player collison interactions
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    public List<string> vegetablesToHold;

    [SerializeField]
    private Image veg1;

    private Sprite lettuce;
    private Sprite carrot;
    private Sprite onion;
    private Sprite redbell;
    private Sprite yellowbell;
    private Sprite tomato;

    void Start()
    {
        lettuce = Resources.Load<Sprite>("Sprites/Lettuce");
        carrot = Resources.Load<Sprite>("Sprites/Carrot");
        redbell = Resources.Load<Sprite>("Sprites/RedBellPepper");
        yellowbell = Resources.Load<Sprite>("Sprites/YellowBellPepper");
        onion = Resources.Load<Sprite>("Sprites/Onion");
        tomato = Resources.Load<Sprite>("Sprites/Tomato");
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;
        PlayerActivity pa = player.GetComponent<PlayerActivity>();

        //Player is holding more than 1 vegetable so they cannot use plate
        if (vegetablesToHold.Count == 1 && pa.vegetables.Count == 1)
        {
            return;
        }

        //There is already a vegetable on this plate
        if (vegetablesToHold.Count == 1 && pa.vegetables.Count == 1)
        {
            return;
        }

        //Add vegetable to plate
        if (vegetablesToHold.Count == 0 && pa.vegetables.Count == 1)
        {
            vegetablesToHold.Add(pa.vegetables[0]);
            pa.vegetables = new List<string>();
            pa.veg1.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
            veg1.sprite = AssignVegetable(vegetablesToHold[0]);
        }
        //Give vegetable back to player
        else if (vegetablesToHold.Count == 1 && pa.vegetables.Count == 0)
        {
            pa.vegetables.Add(vegetablesToHold[0]);
            vegetablesToHold = new List<string>();
            pa.veg1.sprite = AssignVegetable(pa.vegetables[0]);
            veg1.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
        }
    }

    private Sprite AssignVegetable(string vegName)
    {
        Sprite toReturn = Resources.Load<Sprite>("Sprites/emptySprite");
        switch (vegName)
        {
            case "Lettuce":
                toReturn = lettuce;
                break;
            case "Carrot":
                toReturn = carrot;
                break;
            case "Onion":
                toReturn = onion;
                break;
            case "RedBellPepper":
                toReturn = redbell;
                break;
            case "YellowBellPepper":
                toReturn = yellowbell;
                break;
            case "Tomato":
                toReturn = tomato;
                break;
        }
        return toReturn;
    }
}
