using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas TimerCanvas;

    private string[] timers = new string[] { "ChopTimer1", "ChopTimer2", "CustomerTimer1", "CustomerTimer2", "CustomerTimer3", "CustomerTimer4", "CustomerTimer5" };

    private Transform chopTimer1;
    private Transform chopTimer2;
    private Transform customerTimer1;
    private Transform customerTimer2;
    private Transform customerTimer3;
    private Transform customerTimer4;
    private Transform customerTimer5;

    private bool isTwo;

    private void Awake()
    {
        if (TimerCanvas != null)
        {
            AssignTimers();
            InitialCustomers();
        }
        else
        {
            Debug.Log("Please add the TimerCanvas from the Hierarchy to the associated variable on this script.");
        }
    }
    void Start()
    {
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

        customerTimer1 = TimerCanvas.transform.Find("CustomerTimer1");
        customerTimer2 = TimerCanvas.transform.Find("CustomerTimer2");
        customerTimer3 = TimerCanvas.transform.Find("CustomerTimer3");
        customerTimer4 = TimerCanvas.transform.Find("CustomerTimer4");
        customerTimer5 = TimerCanvas.transform.Find("CustomerTimer5");
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

    private void ChopTimer1Start()
    {
       chopTimer1.gameObject.SetActive(true);
    }

    private void ChopTimer2Start()
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
            im.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            Timer t = img.GetComponent<Timer>();
            t.time = t.timeLeft;
            isTwo = false;
        }
        else
        {
            Transform img2 = chopTimer1.Find("Veg1");
            Image im = img2.GetComponent<Image>();
            im.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            Transform img3 = chopTimer1.Find("Veg2");
            Image im3 = img2.GetComponent<Image>();
            im3.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
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
            im.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            Timer t = img.GetComponent<Timer>();
            t.time = t.timeLeft;
            isTwo = false;
        }
        else
        {
            Transform img2 = chopTimer2.Find("Veg1");
            Image im = img2.GetComponent<Image>();
            im.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            Transform img3 = chopTimer2.Find("Veg2");
            Image im3 = img2.GetComponent<Image>();
            im3.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            chopTimer2.gameObject.SetActive(false);
        }
    }

    void SubtractPointsBothPlayers()
    {
        GameObject p1 = GameObject.Find("Player1");
        GameObject p2 = GameObject.Find("Player2");
        SubtractPoints(p1, 5);
        SubtractPoints(p2, 5);
    }

    void SubtractPointsP1()
    {
        GameObject p1 = GameObject.Find("Player1");
        SubtractPoints(p1, 10);
    }

    void SubtractPointsP2()
    {
        GameObject p2 = GameObject.Find("Player2");
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
