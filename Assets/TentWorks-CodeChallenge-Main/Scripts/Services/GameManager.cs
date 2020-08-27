///-----------------------------------------------------------------
///   Class:          GameManager
///   Description:    This script manages 'big picture' interactions in the game, mainly via the Notification Center
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
#region using directives
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas TimerCanvas;

    private GameObject p1;
    private GameObject p2;

    private Transform chopTimer1;
    private Transform chopTimer2;

    private bool isTwo;

    private void Awake()
    {
        if (TimerCanvas != null)
        {
            AssignTimers();
            InitialCustomers();
        }
    }
    void Start()
    {
        p1 = GameObject.Find("Player1");
        p2 = GameObject.Find("Player2");

        NotificationCenter.DefaultCenter.AddObserver(this, "CustomerTimer1Ended");
        NotificationCenter.DefaultCenter.AddObserver(this, "CustomerTimer2Ended");
        NotificationCenter.DefaultCenter.AddObserver(this, "CustomerTimer3Ended");
        NotificationCenter.DefaultCenter.AddObserver(this, "CustomerTimer4Ended");
        NotificationCenter.DefaultCenter.AddObserver(this, "CustomerTimer5Ended");
        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer1Ended");
        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer2Ended");

        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer1Start");
        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer2Start");

        NotificationCenter.DefaultCenter.AddObserver(this, "IsTwo");

        NotificationCenter.DefaultCenter.AddObserver(this, "SubtractPointsP1");
        NotificationCenter.DefaultCenter.AddObserver(this, "SubtractPointsP2");
        NotificationCenter.DefaultCenter.AddObserver(this, "SubtractPointsBothPlayers");
    }

    private void AssignTimers()
    {
        chopTimer1 = TimerCanvas.transform.Find("ChopTimer1");
        chopTimer1.gameObject.SetActive(false);

        chopTimer2 = TimerCanvas.transform.Find("ChopTimer2");
        chopTimer2.gameObject.SetActive(false);
    }

    private void InitialCustomers()
    {
        //When we instantiate customers pass along the slot number into the public class
        int xPos = -4;
        Vector3 spawnPos = new Vector3(-4, .3f, 3.6f);
        for (int i = 0; i < 5; i++)
        {
            spawnPos.x = xPos;
            GameObject go = Instantiate(Resources.Load("Prefabs/Customer"), spawnPos, Quaternion.identity) as GameObject;
            Customer c = go.GetComponent<Customer>();
            c.slotNum = i;
            xPos += 2;
        }
    }

    //--Methods below are all called via Notification system--//
    void ChopTimer1Start()
    {
       chopTimer1.gameObject.SetActive(true);
    }

    void ChopTimer2Start()
    {
        chopTimer2.gameObject.SetActive(true);
    }

    void CustomerTimer1Ended()
    {
        GameObject c1 = GameObject.Find("Customer 1");
        Destroy(c1);

        Vector3 spawnPos = new Vector3(-4, .3f, 3.6f);

        GameObject go = Instantiate(Resources.Load("Prefabs/Customer"), spawnPos, Quaternion.identity) as GameObject;
        Customer c = go.GetComponent<Customer>();
        c.slotNum = 0;
    }

    void CustomerTimer2Ended()
    {
        GameObject c2 = GameObject.Find("Customer 2");
        Destroy(c2);

        Vector3 spawnPos = new Vector3(-2, .3f, 3.6f);

        GameObject go = Instantiate(Resources.Load("Prefabs/Customer"), spawnPos, Quaternion.identity) as GameObject;
        Customer c = go.GetComponent<Customer>();
        c.slotNum = 1;
    }

    void CustomerTimer3Ended()
    {
        GameObject c3 = GameObject.Find("Customer 3");
        Destroy(c3);

        Vector3 spawnPos = new Vector3(0, .3f, 3.6f);

        GameObject go = Instantiate(Resources.Load("Prefabs/Customer"), spawnPos, Quaternion.identity) as GameObject;
        Customer c = go.GetComponent<Customer>();
        c.slotNum = 2;
    }

    void CustomerTimer4Ended()
    {
        GameObject c4 = GameObject.Find("Customer 4");
        Destroy(c4);

        Vector3 spawnPos = new Vector3(2, .3f, 3.6f);

        GameObject go = Instantiate(Resources.Load("Prefabs/Customer"), spawnPos, Quaternion.identity) as GameObject;
        Customer c = go.GetComponent<Customer>();
        c.slotNum = 3;
    }

    void CustomerTimer5Ended()
    {
        GameObject c5 = GameObject.Find("Customer 5");
        Destroy(c5);

        Vector3 spawnPos = new Vector3(4, .3f, 3.6f);

        GameObject go = Instantiate(Resources.Load("Prefabs/Customer"), spawnPos, Quaternion.identity) as GameObject;
        Customer c = go.GetComponent<Customer>();
        c.slotNum = 4;
    }

    void ChopTimer1Ended()
    {
        if (isTwo == true)
        {
            Transform img = chopTimer1.Find("Image");
            Transform img2 = chopTimer1.Find("Veg1");
            Image im = img2.GetComponent<Image>();
            im.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
            Timer t = img.GetComponent<Timer>();
            t.time = t.timeLeft;
            isTwo = false;
        }
        else
        {
            Transform img2 = chopTimer1.Find("Veg1");
            Image im = img2.GetComponent<Image>();
            im.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
            Transform img3 = chopTimer1.Find("Veg2");
            Image im3 = img2.GetComponent<Image>();
            im3.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
            chopTimer1.gameObject.SetActive(false);
        }
    }
    void ChopTimer2Ended()
    {
        if (isTwo == true)
        {
            Transform img = chopTimer2.Find("Image");
            Transform img2 = chopTimer2.Find("Veg1");
            Image im = img2.GetComponent<Image>();
            im.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
            Timer t = img.GetComponent<Timer>();
            t.time = t.timeLeft;
            isTwo = false;
        }
        else
        {
            Transform img2 = chopTimer2.Find("Veg1");
            Image im = img2.GetComponent<Image>();
            im.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
            Transform img3 = chopTimer2.Find("Veg2");
            Image im3 = img2.GetComponent<Image>();
            im3.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
            chopTimer2.gameObject.SetActive(false);
        }
    }

    void SubtractPointsBothPlayers()
    {
        p1 = GameObject.Find("Player1");
        p2 = GameObject.Find("Player2");
        SubtractPoints(p1, 5);
        SubtractPoints(p2, 5);
    }

    void SubtractPointsP1()
    {
        p1 = GameObject.Find("Player1");
        SubtractPoints(p1, 10);
    }

    void SubtractPointsP2()
    {
        p2 = GameObject.Find("Player2");
        SubtractPoints(p2, 10);
    }

    void IsTwo()
    {
        isTwo = true;
    }

    private void SubtractPoints(GameObject player, int points)
    {
        PlayerActivity pa = player.gameObject.GetComponent<PlayerActivity>();
        pa.score -= points;
        NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
    }
}
