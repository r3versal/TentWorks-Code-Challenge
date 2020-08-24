using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CuttingBoard : MonoBehaviour
{
    public List<string> vegetablesToChop;
    public List<string> vegetablesChopped;

    public Image veg1;
    public Image veg2;

    private Sprite lettuce;
    private Sprite carrot;
    private Sprite onion;
    private Sprite redbell;
    private Sprite orangebell;
    private Sprite tomato;


    // Start is called before the first frame update
    void Start()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer1Ended");
        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer2Ended");

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
        vegetablesToChop = pa.vegetables;
        pa.vegetables = new List<string>();
        pa.veg1.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        pa.veg2.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");

        if(vegetablesToChop.Count == 0)
        {
            //give player veggies to deliver
            NotificationCenter.DefaultCenter.PostNotification(this, "PickedUpIngredients");
        }
        if(vegetablesToChop.Count == 1)
        {
          veg1.sprite = AssignVegetable(vegetablesToChop[0]);
        }
        if (vegetablesToChop.Count == 2)
        {
            veg1.sprite = AssignVegetable(vegetablesToChop[0]);
            veg2.sprite = AssignVegetable(vegetablesToChop[1]);
        }

        foreach (string s in vegetablesToChop)
        {
            vegetablesChopped.Add(s);
        }

       if (gameObject.name == "CuttingBoard1")
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "ChopTimer1Start");
        }
        if (gameObject.name == "CuttingBoard2")
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "ChopTimer2Start");
        }

        if (vegetablesToChop.Count == 2)
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "IsTwo");
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
