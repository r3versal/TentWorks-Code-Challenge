///-----------------------------------------------------------------
///   Class:          PlayerActivity
///   Description:    This script is responsible for player interactions
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
#region using directives
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class PlayerActivity : MonoBehaviour
{
    private Text playerTimeText;
    private Text playerScoreText;
    private string playerNum;
    private string p1;
    
    public int score;

    public float time;
    public int gameTime = 250;

    private bool oneTime;
    private bool canDeliver;

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
        if (playerNum == "Player1")
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
        time = gameTime;

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
            //Store chopped vegetables in case we recieve notification that they are ready to deliver to Customer
            if (go.name.Contains("Board") && go.name.Contains(p1))
            {
                CuttingBoard cb = go.GetComponent<CuttingBoard>();
                ingredients = cb.vegetablesChopped;
            }
            //Pickup a vegetable
            if (go.tag == "Vegetable")
            {
                if (vegetables.Count == 2)
                {
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
            //Check if we collided with a customer
            CheckCustomerCollision(go);
        }
    }

    private void CheckCustomerCollision(GameObject go)
    {
        GameObject timerCanvas = GameObject.FindGameObjectWithTag("Timer");
        string goName = go.gameObject.name;

        if (goName.Contains("Customer") && gameObject.name == "Player1")
        {
           switch (goName)
            {
                case "CustomerCollider 1":
                    CustomerInteraction(timerCanvas, "Customer 1", "CustomerTimer1/Image", "CustomerTimer1Ended", "UICustomer1Angry", "SpawnBonus");
                    break;
                case "CustomerCollider 2":
                    CustomerInteraction(timerCanvas, "Customer 2", "CustomerTimer2/Image", "CustomerTimer2Ended", "UICustomer2Angry", "SpawnBonus");
                    break;
                case "CustomerCollider 3":
                    CustomerInteraction(timerCanvas, "Customer 3", "CustomerTimer3/Image", "CustomerTimer3Ended", "UICustomer3Angry", "SpawnBonus");
                    break;
                case "CustomerCollider 4":
                    CustomerInteraction(timerCanvas, "Customer 4", "CustomerTimer4/Image", "CustomerTimer4Ended", "UICustomer4Angry", "SpawnBonus");
                    break;
                case "CustomerCollider 5":
                    CustomerInteraction(timerCanvas, "Customer 5", "CustomerTimer5/Image", "CustomerTimer5Ended", "UICustomer5Angry", "SpawnBonus");
                    break;
            }
        }
        else if (goName.Contains("Customer") && gameObject.name == "Player2")
        {
            switch (goName)
            {
                case "CustomerCollider 1":
                    CustomerInteraction(timerCanvas, "Customer 1", "CustomerTimer1/Image", "CustomerTimer1Ended", "UICustomer1Angry", "SpawnBonusP2");
                    break;
                case "CustomerCollider 2":
                    CustomerInteraction(timerCanvas, "Customer 2", "CustomerTimer2/Image", "CustomerTimer2Ended", "UICustomer2Angry", "SpawnBonusP2");
                    break;
                case "CustomerCollider 3":
                    CustomerInteraction(timerCanvas, "Customer 3", "CustomerTimer3/Image", "CustomerTimer3Ended", "UICustomer3Angry", "SpawnBonusP2");
                    break;
                case "CustomerCollider 4":
                    CustomerInteraction(timerCanvas, "Customer 4", "CustomerTimer4/Image", "CustomerTimer4Ended", "UICustomer4Angry", "SpawnBonusP2");
                    break;
                case "CustomerCollider 5":
                    CustomerInteraction(timerCanvas, "Customer 5", "CustomerTimer5/Image", "CustomerTimer5Ended", "UICustomer5Angry", "SpawnBonusP2");
                    break;
            }
        }
    }

    private void CustomerInteraction(GameObject timerCanvas, string customerNumber, string customerNumImage, string customerNumTimerEnd, string customerNumAngry, string bonusNum)
    {
        //Get recipe
        GameObject customerGO = GameObject.Find(customerNumber);
        Customer c = customerGO.GetComponent<Customer>();
        string[] customerIngredients = c.ingredients;
        int correct = 0;

        Image customerTimer1 = timerCanvas.transform.Find(customerNumImage).GetComponent<Image>();
        Timer t = customerTimer1.GetComponent<Timer>();

        if (canDeliver == true)
        {
            //Compare ingredients
            foreach (string s in customerIngredients)
            {
                if (ingredients.Contains(s))
                {
                    correct++;
                }
            }
            if (correct == c.ingredients.Length && ingredients.Count() == customerIngredients.Count())
            {
                //Give player some points, send notification to end Customer 1 Timer
                score += 25;
                UpdateScore();
                NotificationCenter.DefaultCenter.PostNotification(this, customerNumTimerEnd);

                //Check if player made it before 70% of time, if so give bonus to player
                if (t.time > c.threshold)//example: t.time > .3 of total time
                {
                    score += 10;
                    UpdateScore();
                    NotificationCenter.DefaultCenter.PostNotification(this, bonusNum);
                }
                else
                {
                    NotificationCenter.DefaultCenter.PostNotification(this, "UICustomerSuccess");
                }
                veg1.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
            }
            else
            {
                UpdateScore();
                NotificationCenter.DefaultCenter.PostNotification(this, customerNumAngry);
                veg1.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
                if (bonusNum == "SpawnBonus")
                {
                    t.isP1 = true;
                }
                t.isAngry = true;
            }
            canDeliver = false;
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

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;

            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            playerTimeText.text = "Time " + minutes + ":" + seconds;
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

    //--Methods below are all called via Notification system--//

    private void UpdateScore()
    {
        playerScoreText.text = "Score : " + score;
    }

    private void PickedUpIngredientsP1()
    {
        if (playerNum == "Player1")
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "UIDeliverSalad");
            veg1.sprite = salad;
            veg2.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
            canDeliver = true;
        }
    }

    private void PickedUpIngredientsP2()
    {
        if (playerNum == "Player2")
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "UIDeliverSalad");
            veg1.sprite = salad;
            veg2.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
            canDeliver = true;
        }
    }
}
