using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    public List<string> vegetablesToHold;

    //Assign in hierarchy
    public Image veg1;

    private Sprite lettuce;
    private Sprite carrot;
    private Sprite onion;
    private Sprite redbell;
    private Sprite orangebell;
    private Sprite tomato;

    void Start()
    {
        lettuce = Resources.Load<Sprite>("Sprites/Lettuce");
        carrot = Resources.Load<Sprite>("Sprites/Carrot");
        redbell = Resources.Load<Sprite>("Sprites/RedBellPepper");
        orangebell = Resources.Load<Sprite>("Sprites/YellowBellPepper");
        onion = Resources.Load<Sprite>("Sprites/Onion");
        tomato = Resources.Load<Sprite>("Sprites/Tomato");
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;
        PlayerActivity pa = player.GetComponent<PlayerActivity>();

        if (vegetablesToHold.Count == 1 && pa.vegetables.Count == 1)
        {
            //TODO: UI for "you have too many vegetables to use plate"
            Debug.Log("Already holding vegetable");
            return;
        }

        if (pa.vegetables.Count > 1)
        {
            //TODO: UI for "you have too many vegetables to use plate"
            Debug.Log("Too many vegetables");
            return;
        }

        if (vegetablesToHold.Count == 0 && pa.vegetables.Count == 1)
        {
            vegetablesToHold.Add(pa.vegetables[0]);
            pa.vegetables = new List<string>();
            pa.veg1.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            veg1.sprite = AssignVegetable(vegetablesToHold[0]);
        }
        else if (vegetablesToHold.Count == 1 && pa.vegetables.Count == 0)
        {
            pa.vegetables.Add(vegetablesToHold[0]);
            vegetablesToHold = new List<string>();
            pa.veg1.sprite = AssignVegetable(pa.vegetables[0]);
            veg1.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        }
    }

    private Sprite AssignVegetable(string vegName)
    {
        Sprite toReturn = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
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
            case "OrangeBellPepper":
                toReturn = orangebell;
                break;
            case "Tomato":
                toReturn = tomato;
                break;
        }
        return toReturn;
    }
}
