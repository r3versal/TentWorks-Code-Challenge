using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActivity : MonoBehaviour
{
    private Text playerTimeText;
    private Text playerScoreText;
    private string playerNum;
    private string p1;
    
    public string name;
    public float score;
    public float time ;
    private bool oneTime;

    public Image veg1;
    public Image veg2;

    private Sprite lettuce;
    private Sprite carrot;
    private Sprite onion;
    private Sprite redbell;
    private Sprite orangebell;
    private Sprite tomato;

    public List<string> vegetables;
    public List<string> ingredients;

    private void Awake()
    {
        playerNum = gameObject.name.Substring(0, 7);
        gameObject.name = playerNum;
        if (name == null)
        {
            name = playerNum;
        }
        p1 = gameObject.name.Substring(6, 1);
    }
    void Start()
    {
        if(playerNum == "Player1")
        {
            //assign text for time and score
            playerTimeText = GameObject.Find("Canvas/Player1 Stats/Timer").GetComponent<Text>();
            playerScoreText = GameObject.Find("Canvas/Player1 Stats/Score").GetComponent<Text>();
            veg1 = GameObject.Find("Canvas/Player1 Stats/VegHolder/Veg1").GetComponent<Image>();
            veg2 = GameObject.Find("Canvas/Player1 Stats/VegHolder/Veg2").GetComponent<Image>();
        }
        else
        {
            //assign text for time and score
            playerTimeText = GameObject.Find("Canvas/Player2 Stats/Timer").GetComponent<Text>();
            playerScoreText = GameObject.Find("Canvas/Player2 Stats/Score").GetComponent<Text>();
            veg1 = GameObject.Find("Canvas/Player2 Stats/VegHolder/Veg1").GetComponent<Image>();
            veg2 = GameObject.Find("Canvas/Player2 Stats/VegHolder/Veg2").GetComponent<Image>();
        }
        //TODO: Update score when system is in place
        score = 12;
        time = Random.Range(15, 30);

        playerScoreText.text = "Score : " + score;
        ingredients = new List<string>();
        vegetables = new List<string>();

        lettuce = Resources.Load<Sprite>("Sprites/Lettuce");
        carrot = Resources.Load<Sprite>("Sprites/Carrot");
        redbell = Resources.Load<Sprite>("Sprites/RedBellPepper");
        orangebell = Resources.Load<Sprite>("Sprites/YellowBellPepper");
        onion = Resources.Load<Sprite>("Sprites/Onion");
        tomato = Resources.Load<Sprite>("Sprites/Tomato");

        NotificationCenter.DefaultCenter.AddObserver(this, "PickedUpIngredients");

        //TODO add notification observer that time has changed via pickup and needs to be updated
        //TODO add notification observer that score has changed and needs to be updated
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        if (go.gameObject.name != "Plane")
        {
            //Store choppedVegetables in case we recieve notification that they are ready to deliver to Customer
            
            if (go.name.Contains("Board") && go.name.Contains(p1))
            {
                CuttingBoard cb = go.GetComponent<CuttingBoard>();
                ingredients = cb.vegetablesChopped;

            }
            if (go.tag == "Vegetable")
            {
                if (vegetables.Count == 2)
                {
                    //TODO UI saying too many vegetables! Use garbage can!
                }
                else
                {
                    vegetables.Add(go.name);
                    if (vegetables.Count == 1)
                    {
                        veg1.sprite = AssignVegetable(go.name);
                    }
                    else
                    {
                        veg2.sprite = AssignVegetable(go.name);
                    }
                }
            }
            CheckCustomerCollision(go);
        }
    }

    private void CheckCustomerCollision(GameObject go)
    {
        string goName = go.gameObject.name;
     if (goName.Contains("Customer"))
        {
            switch (goName)
            {
                case "CustomerCollider 1":
                    //get recipe
                    int correct = 0;
                    GameObject customerGO = GameObject.Find("Customer 1");
                    Customer c = customerGO.GetComponent<Customer>();
                    string[] customerIngredients = c.ingredients;

                    //Compare ingredients
                    foreach(string s in customerIngredients)
                    {
                        if (ingredients.Contains(s))
                        {
                            correct++;
                        }
                    }
                    if(correct == 3)
                    {
                        //TODO: give player some points, send notification to end Customer 1 Timer
                        //TODO: check if we made it before 70% of time, if so give bonus to player
                        Debug.Log("YAY");
                    }
                    else
                    {
                        //TODO: takeaway some player points
                        //TODO: make customer angry
                        Debug.Log("Customer is angry");
                    }
                    break;
                case "Customer 2":
                    break;
                case "Customer 3":
                    break;
                case "Customer 4":
                    break;
                case "Customer 5":
                    break;
            }
        }
    }

    private void PickedUpIngredients()
    {
        Debug.Log("Deliver ingredients!");
        //TODO make sure to add if ingredients.length > 0, otherwise the wrong player will try to access the ingredients
        //spawn salad bowl in player inventory
        //add collision with customerx 
        //cross reference player ingredients with customer recipe
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

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            string seconds = (time % 60).ToString("00");
            playerTimeText.text = "Time : " + seconds;
        }
        else
        {
            if (!oneTime)
            {
                oneTime = true;
                if (p1 == "1")
                {
                    NotificationCenter.DefaultCenter.PostNotification(this, "Savep1Data");
                }
                else
                {
                    NotificationCenter.DefaultCenter.PostNotification(this, "Savep2Data");
                }
            }

        }
    }
}
