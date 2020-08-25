using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActivity : MonoBehaviour
{
    private Text playerTimeText;
    private Text playerScoreText;
    private string playerNum;
    private string p1;
    
    public int score;
    public float time ;
    private bool oneTime;

    public Image veg1;
    public Image veg2;

    private Sprite salad;
    private Sprite lettuce;
    private Sprite carrot;
    private Sprite onion;
    private Sprite redbell;
    private Sprite yellowbell;
    private Sprite tomato;

    public List<string> vegetables;
    public List<string> ingredients;



    private void Awake()
    {
        playerNum = gameObject.name.Substring(0, 7);
        gameObject.name = playerNum;
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
        score = 0;
        time = 30;

        playerScoreText.text = "Score : " + score;
        ingredients = new List<string>();
        vegetables = new List<string>();

        salad = Resources.Load<Sprite>("Sprites/Salad");
        lettuce = Resources.Load<Sprite>("Sprites/Lettuce");
        carrot = Resources.Load<Sprite>("Sprites/Carrot");
        redbell = Resources.Load<Sprite>("Sprites/RedBellPepper");
        yellowbell = Resources.Load<Sprite>("Sprites/YellowBellPepper");
        onion = Resources.Load<Sprite>("Sprites/Onion");
        tomato = Resources.Load<Sprite>("Sprites/Tomato");

        NotificationCenter.DefaultCenter.AddObserver(this, "PickedUpIngredientsP1");
        NotificationCenter.DefaultCenter.AddObserver(this, "PickedUpIngredientsP2");
        NotificationCenter.DefaultCenter.AddObserver(this, "UpdateScore");
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
                    //UI - too many vegetables! Use garbage can!
                    NotificationCenter.DefaultCenter.PostNotification(this, "UIPlateTooMany");
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
        GameObject timerCanvas = GameObject.FindGameObjectWithTag("Timer");
        string goName = go.gameObject.name;
        int correct = 0;

        if (goName.Contains("Customer"))
        {
            switch (goName)
            {
                case "CustomerCollider 1":
                    //get recipe
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
                        //Give player some points, send notification to end Customer 1 Timer
                        score = +25;
                        NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer1Ended");

                        //Check if player made it before 70% of time, if so give bonus to player
                        Image customerTimer1 = timerCanvas.transform.Find("CustomerTimer1/Image").GetComponent<Image>();
                        Timer t = customerTimer1.GetComponent<Timer>();

                        //TODO: determine if this logic is right
                        if (t.timeLeft <= c.threshold)
                        {
                            score += 10;
                            NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
                            NotificationCenter.DefaultCenter.PostNotification(this, "SpawnBonus");
                        }
                        NotificationCenter.DefaultCenter.PostNotification(this, "UICustomerSuccess");
                    }
                    else
                    {
                        score -= 25;
                        NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
                        NotificationCenter.DefaultCenter.PostNotification(this, "UICustomer1Angry");
                    }
                    break;
                case "CustomerCollider 2":
                    GameObject customerGO2 = GameObject.Find("Customer 2");
                    Customer c2 = customerGO2.GetComponent<Customer>();
                    string[] customerIngredients2 = c2.ingredients;

                    //Compare ingredients
                    foreach (string s in customerIngredients2)
                    {
                        if (ingredients.Contains(s))
                        {
                            correct++;
                        }
                    }
                    if (correct == 3)
                    {
                        //Give player some points, send notification to end Customer 1 Timer
                        score = +25;
                        NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer2Ended");

                        //Check if player made it before 70% of time, if so give bonus to player
                        Image customerTimer2 = timerCanvas.transform.Find("CustomerTimer2/Image").GetComponent<Image>();
                        Timer t = customerTimer2.GetComponent<Timer>();

                        //TODO: determine if this logic is right
                        if (t.timeLeft <= c2.threshold)
                        {
                            score += 10;
                            NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
                            NotificationCenter.DefaultCenter.PostNotification(this, "SpawnBonus");
                        }
                        NotificationCenter.DefaultCenter.PostNotification(this, "UICustomerSuccess");
                    }
                    else
                    {
                        score -= 25;
                        NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
                        NotificationCenter.DefaultCenter.PostNotification(this, "UICustomer2Angry");
                    }
                    break;
                case "CustomerCollider 3":
                    GameObject customerGO3 = GameObject.Find("Customer 3");
                    Customer c3 = customerGO3.GetComponent<Customer>();
                    string[] customerIngredients3 = c3.ingredients;

                    //Compare ingredients
                    foreach (string s in customerIngredients3)
                    {
                        if (ingredients.Contains(s))
                        {
                            correct++;
                        }
                    }
                    if (correct == 3)
                    {
                        //Give player some points, send notification to end Customer 1 Timer
                        score = +25;
                        NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer3Ended");

                        //Check if player made it before 70% of time, if so give bonus to player
                        Image customerTimer2 = timerCanvas.transform.Find("CustomerTimer3/Image").GetComponent<Image>();
                        Timer t = customerTimer2.GetComponent<Timer>();

                        //TODO: determine if this logic is right
                        if (t.timeLeft <= c3.threshold)
                        {
                            score += 10;
                            NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
                            NotificationCenter.DefaultCenter.PostNotification(this, "SpawnBonus");
                        }
                        NotificationCenter.DefaultCenter.PostNotification(this, "UICustomerSuccess");
                    }
                    else
                    {
                        score -= 25;
                        NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
                        NotificationCenter.DefaultCenter.PostNotification(this, "UICustomer3Angry");
                    }
                    break;
                case "CustomerCollider 4":
                    GameObject customerGO4 = GameObject.Find("Customer 4");
                    Customer c4 = customerGO4.GetComponent<Customer>();
                    string[] customerIngredients4 = c4.ingredients;

                    //Compare ingredients
                    foreach (string s in customerIngredients4)
                    {
                        if (ingredients.Contains(s))
                        {
                            correct++;
                        }
                    }
                    if (correct == 3)
                    {
                        //Give player some points, send notification to end Customer 1 Timer
                        score = +25;
                        NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer4Ended");

                        //Check if player made it before 70% of time, if so give bonus to player
                        Image customerTimer2 = timerCanvas.transform.Find("CustomerTimer4/Image").GetComponent<Image>();
                        Timer t = customerTimer2.GetComponent<Timer>();

                        //TODO: determine if this logic is right
                        if (t.timeLeft <= c4.threshold)
                        {
                            score += 10;
                            NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
                            NotificationCenter.DefaultCenter.PostNotification(this, "SpawnBonus");
                        }
                        NotificationCenter.DefaultCenter.PostNotification(this, "UICustomerSuccess");
                    }
                    else
                    {
                        score -= 25;
                        NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
                        NotificationCenter.DefaultCenter.PostNotification(this, "UICustomer4Angry");
                    }
                    break;
                case "CustomerCollider 5":
                    GameObject customerGO5 = GameObject.Find("Customer 5");
                    Customer c5 = customerGO5.GetComponent<Customer>();
                    string[] customerIngredients5 = c5.ingredients;

                    //Compare ingredients
                    foreach (string s in customerIngredients5)
                    {
                        if (ingredients.Contains(s))
                        {
                            correct++;
                        }
                    }
                    if (correct == 3)
                    {
                        //Give player some points, send notification to end Customer 1 Timer
                        score = +25;
                        NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer5Ended");

                        //Check if player made it before 70% of time, if so give bonus to player
                        Image customerTimer2 = timerCanvas.transform.Find("CustomerTimer5/Image").GetComponent<Image>();
                        Timer t = customerTimer2.GetComponent<Timer>();

                        //TODO: determine if this logic is right
                        if (t.timeLeft <= c5.threshold)
                        {
                            score += 10;
                            NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
                            NotificationCenter.DefaultCenter.PostNotification(this, "SpawnBonus");
                        }
                        NotificationCenter.DefaultCenter.PostNotification(this, "UICustomerSuccess");
                    }
                    else
                    {
                        score -= 25;
                        NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
                        NotificationCenter.DefaultCenter.PostNotification(this, "UICustomer5Angry");
                    }
                    break;
            }
        }
    }

    private void UpdateScore()
    {
        playerScoreText.text = "Score : " + score;
    }

    private void PickedUpIngredientsP1()
    {
        if (playerNum == "Player1")
        {
            Debug.Log("Deliver ingredients!");
            NotificationCenter.DefaultCenter.PostNotification(this, "UIDeliverSalad");
            //Spawn salad bowl in player inventory
            veg1.sprite = salad;
            veg2.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        }

    }

    private void PickedUpIngredientsP2()
    {
        if (playerNum == "Player2")
        {
            Debug.Log("Deliver ingredients!");
            NotificationCenter.DefaultCenter.PostNotification(this, "UIDeliverSalad");
            //Spawn salad bowl in player inventory
            veg1.sprite = salad;
            veg2.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
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
