///-----------------------------------------------------------------
///   Class:          Timer
///   Description:    This script is attached to the Image object on every timer, utilizes notification system for actions related to timers ending
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
#region using directives
using UnityEngine;
using UnityEngine.UI;
#endregion

public class Timer : MonoBehaviour
{
    private Image fillImage;
    public float timeLeft;
    public float time;
    public Text timeText;
    public Text recipeText;

    public bool isAngry;
    public bool isP1;
    public bool reset;

    void Start()
    {
        reset = false;
        fillImage = this.GetComponent<Image>();

        //This is how we know we are a chopping board timer not a customer timer
        if(timeLeft == 0)
        {
            timeLeft = 5;
        }
        time = timeLeft;
    }

    void Update()
    {
        if (reset)
        {
            time = timeLeft;
            reset = false;
            isAngry = false;
            isP1 = false;
        }
        if (time > 0)
        {
            if (isAngry)
            {
                time -= Time.deltaTime * 2;
            }
            else
            {
                time -= Time.deltaTime;
            }
            fillImage.fillAmount = time / timeLeft;
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            timeText.text = "Time " + minutes + ":" + seconds;
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
                    if (isAngry)
                    {
                        if (isP1)
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsP1");
                        }
                        else
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsP2");
                        }
                    }
                    else
                    {
                        NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsBothPlayers");
                    }
                    break;
                case "CustomerTimer2":
                    NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer2Ended");
                    if (isAngry)
                    {
                        if (isP1)
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsP1");
                        }
                        else
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsP2");
                        }
                    }
                    else
                    {
                     NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsBothPlayers");
                    }
                    break;
                case "CustomerTimer3":
                    NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer3Ended");
                    if (isAngry)
                    {
                        if (isP1)
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsP1");
                        }
                        else
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsP2");
                        }
                    }
                    else
                    {
                        NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsBothPlayers");
                    }
                    break;
                case "CustomerTimer4":
                    NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer4Ended");
                    if (isAngry)
                    {
                        if (isP1)
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsP1");
                        }
                        else
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsP2");
                        }
                    }
                    else
                    {
                        NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsBothPlayers");
                    }
                    break;
                case "CustomerTimer5":
                    NotificationCenter.DefaultCenter.PostNotification(this, "CustomerTimer5Ended");
                    if (isAngry)
                    {
                        if (isP1)
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsP1");
                        }
                        else
                        {
                            NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsP2");
                        }
                    }
                    else
                    {
                        NotificationCenter.DefaultCenter.PostNotification(this, "SubtractPointsBothPlayers");
                    }
                    break;

            }
            
        }
        
    }
}
