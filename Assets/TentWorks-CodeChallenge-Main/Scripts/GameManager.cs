using System.Collections;
using System.Collections.Generic;
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
       //Destory current customer in slot1
       //instantiate new customer in slot1
    }

    void CustomerTimer2Ended()
    {
        Debug.Log("TIMES UP");
    }

    void CustomerTimer3Ended()
    {
        Debug.Log("TIMES UP");
    }

    void CustomerTimer4Ended()
    {
        Debug.Log("TIMES UP");
    }

    void CustomerTimer5Ended()
    {
        Debug.Log("TIMES UP");
    }

    void ChopTimer1Ended()
    {
        if (isTwo == true)
        {
            Debug.Log("Two Vegetables");
            Transform img = chopTimer1.Find("Image");
            Timer t = img.GetComponent<Timer>();
            t.time = t.timeLeft;
            isTwo = false;
        }
        else
        {
            chopTimer1.gameObject.SetActive(false);
        }
    }
    void ChopTimer2Ended()
    {
        chopTimer2.gameObject.SetActive(false);
    }

    void IsTwo()
    {
        isTwo = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
