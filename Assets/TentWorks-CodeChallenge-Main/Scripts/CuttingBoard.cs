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

    public Image chopTimer1;
    public Image chopTimer2;

    private Sprite lettuce;
    private Sprite carrot;
    private Sprite onion;
    private Sprite redbell;
    private Sprite yellowbell;
    private Sprite tomato;

    void Start()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer1Ended");
        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer2Ended");

        lettuce = Resources.Load<Sprite>("Sprites/Lettuce");
        carrot = Resources.Load<Sprite>("Sprites/Carrot");
        redbell = Resources.Load<Sprite>("Sprites/RedBellPepper");
        yellowbell = Resources.Load<Sprite>("Sprites/YellowBellPepper");
        onion = Resources.Load<Sprite>("Sprites/Onion");
        tomato = Resources.Load<Sprite>("Sprites/Tomato");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Timer t = new Timer();
        GameObject player = collision.gameObject;
        PlayerActivity pa = player.GetComponent<PlayerActivity>();
        vegetablesToChop = pa.vegetables;
        pa.vegetables = new List<string>();
        pa.veg1.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        pa.veg2.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        string rString = "";

        if (vegetablesToChop.Count == 0)
        {
            //Give player veggies to deliver
            if(player.name == "Player1")
            {
                NotificationCenter.DefaultCenter.PostNotification(this, "PickedUpIngredientsP1");
            }
            else
            {
                NotificationCenter.DefaultCenter.PostNotification(this, "PickedUpIngredientsP2");
            }
            vegetablesChopped = new List<string>();
            rString = "";
            updateUI(t, rString);
            return;
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

        //TODO: optimize these loops
        
        foreach(string s in vegetablesToChop)
        {
            vegetablesChopped.Add(s);
        }
        for (int i = 0; i < vegetablesChopped.Count; i++)
        {
            string s = vegetablesChopped[i];
            
            if (i == vegetablesChopped.Count - 1)
            {
                rString += s;
            }
            else
            {
                rString += s + ", ";
            }
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

        updateUI(t, rString);
    }

    private void updateUI(Timer t, string rString)
    {
        if (gameObject.name.Contains("1"))
        {
            t = chopTimer1.GetComponent<Timer>();
            t.timeLeft = 5;
            t.reset = true;
            t.recipeText.text = rString;
        }
        if (gameObject.name.Contains("2"))
        {
            t = chopTimer2.GetComponent<Timer>();
            t.timeLeft = 5;
            t.reset = true;
            t.recipeText.text = rString;
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
