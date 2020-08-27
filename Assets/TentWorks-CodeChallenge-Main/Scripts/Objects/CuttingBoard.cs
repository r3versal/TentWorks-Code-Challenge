///-----------------------------------------------------------------
///   Class:          CuttingBoard
///   Description:    This script is attached to the 3D collision object for the Cutting Board, it handles all events associated with a Player/Cutting Board collision interaction
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
#region using directives
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class CuttingBoard : MonoBehaviour
{
    public List<string> vegetablesToChop;
    public List<string> vegetablesChopped;

    //Assign in Hierarchy
    public Image veg1;
    public Image veg2;

    //Assign in Hierarchy
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
        GameObject player = collision.gameObject;
        PlayerActivity pa = player.GetComponent<PlayerActivity>();
        vegetablesToChop = pa.vegetables;
        pa.vegetables = new List<string>();
        pa.veg1.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
        pa.veg2.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
        string rString = "";

        //Give player veggies to deliver
        if (vegetablesToChop.Count == 0 && vegetablesChopped.Count > 0)
        {
            if(player.name == "Player1")
            {
                NotificationCenter.DefaultCenter.PostNotification(this, "PickedUpIngredientsP1");
            }
            else
            {
                NotificationCenter.DefaultCenter.PostNotification(this, "PickedUpIngredientsP2");
            }
            veg1.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
            veg2.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
            vegetablesChopped = new List<string>();
            rString = "";
            updateUI(rString);
            return;
        }
        //Chop 1 vegetable
        if(vegetablesToChop.Count == 1)
        {
          veg1.sprite = AssignVegetable(vegetablesToChop[0]);
        }
        //Chop 2 vegetables
        if (vegetablesToChop.Count == 2)
        {
            veg1.sprite = AssignVegetable(vegetablesToChop[0]);
            veg2.sprite = AssignVegetable(vegetablesToChop[1]);
        }

        //Manage stored vegetables
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

        if (gameObject.name == "CuttingBoard1" && vegetablesToChop.Count > 0)
        {
            if(player.name == "Player1")
            {
                NotificationCenter.DefaultCenter.PostNotification(this, "ChopTimer1Start");
                updateUI(rString);
            }
            else
            {
                NotificationCenter.DefaultCenter.PostNotification(this, "UIMustUseOwnP1");
            }
        }
        if (gameObject.name == "CuttingBoard2" && vegetablesToChop.Count > 0)
        {
            if(player.name == "Player2")
            {
                NotificationCenter.DefaultCenter.PostNotification(this, "ChopTimer2Start");
                updateUI(rString);
            }
            else
            {
                NotificationCenter.DefaultCenter.PostNotification(this, "UIMustUseOwnP2");
            }
        }
        //So we know to run the timer twice
        if (vegetablesToChop.Count == 2)
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "IsTwo");
        }
    }

    private void updateUI(string rString)
    {
        Timer t;
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
