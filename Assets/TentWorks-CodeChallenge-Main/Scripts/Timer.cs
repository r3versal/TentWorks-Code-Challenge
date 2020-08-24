using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Image fillImage;
    public float timeLeft;
    public float time;
    public Text timeText;
    public Text recipeText;

    public bool reset;

    void Start()
    {
        reset = false;
        fillImage = this.GetComponent<Image>();

        //This is how we know we are a chopping board timer
        if(timeLeft == 0)
        {
            timeLeft = 10;
        }
        time = timeLeft;
    }

    void Update()
    {
        if (reset)
        {
            time = timeLeft;
            reset = false;
        }
        if (time > 0)
        {
            time -= Time.deltaTime;
            fillImage.fillAmount = time / timeLeft; // Example: 9/10, 8/10......0/10
            timeText.text = "Time : " + time.ToString("F");
        }
        else
        {
            string notificationToSend = gameObject.transform.parent.name;
            switch (notificationToSend)
            {
                case "ChopTimer1":
                    NotificationCenter.DefaultCenter.PostNotification(this, "ChopTimer1Ended");
                    break;
                case "ChopTimer2":
                    NotificationCenter.DefaultCenter.PostNotification(this, "ChopTimer2Ended");
                    break;
                case "CustomerTimer1":
                    NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer1Ended");
                    Debug.Log("test");
                    break;
                case "CustomerTimer2":
                    NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer2Ended");
                    break;
                case "CustomerTimer3":
                    NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer3Ended");
                    break;
                case "CustomerTimer4":
                    NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer4Ended");
                    break;
                case "CustomerTimer5":
                    NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer5Ended");
                    break;

            }
            
        }
        
    }
}
