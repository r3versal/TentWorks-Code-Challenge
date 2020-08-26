using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicPopupController : MonoBehaviour
{
    public Image Popup;
    public Text popText;

    public Canvas EndGameCanvas;

    private float time;
    private bool isActive;

    void Start()
    {
        EndGameCanvas.enabled = false;
        Popup.enabled = false;
        popText.enabled = false;
        NotificationCenter.DefaultCenter.AddObserver(this, "UIBonusScore");
        NotificationCenter.DefaultCenter.AddObserver(this, "UIBonusSpeed");
        NotificationCenter.DefaultCenter.AddObserver(this, "UIBonusTime");
        NotificationCenter.DefaultCenter.AddObserver(this, "UIDeliverSalad");
        NotificationCenter.DefaultCenter.AddObserver(this, "UICustomer1Angry");
        NotificationCenter.DefaultCenter.AddObserver(this, "UICustomer2Angry");
        NotificationCenter.DefaultCenter.AddObserver(this, "UICustomer3Angry");
        NotificationCenter.DefaultCenter.AddObserver(this, "UICustomer4Angry");
        NotificationCenter.DefaultCenter.AddObserver(this, "UICustomer5Angry");
        NotificationCenter.DefaultCenter.AddObserver(this, "UICustomerSuccess");
        NotificationCenter.DefaultCenter.AddObserver(this, "UIEndGameP1");
        NotificationCenter.DefaultCenter.AddObserver(this, "UIEndGameP2");

        time = 3;
    }

    void UIBonusScore()
    {
        isActive = true;
        Popup.enabled = true;
        popText.enabled = true;
        popText.text = "Pickup the red Score Bonus!";
    }

    void UIBonusSpeed()
    {
        isActive = true;
        Popup.enabled = true;
        popText.enabled = true;
        popText.text = "Pickup the red Speed Bonus!";
    }

    void UIBonusTime()
    {
        isActive = true;
        Popup.enabled = true;
        popText.enabled = true;
        popText.text = "Pickup the red Time Bonus!";
    }

    void UIDeliverSalad()
    {
        isActive = true;
        Popup.enabled = true;
        popText.enabled = true;
        popText.text = "Deliver to Customer!";
    }

    void UICustomer1Angry()
    {
        isActive = true;
        Popup.enabled = true;
        popText.enabled = true;
        popText.text = "Customer 1 is angry   >:(";
    }

    void UICustomer2Angry()
    {
        isActive = true;
        Popup.enabled = true;
        popText.enabled = true;
        popText.text = "Customer 2 is angry   >:(";
    }

    void UICustomer3Angry()
    {
        isActive = true;
        Popup.enabled = true;
        popText.enabled = true;
        popText.text = "Customer 3 is angry   >:(";
    }

    void UICustomer4Angry()
    {
        isActive = true;
        Popup.enabled = true;
        popText.enabled = true;
        popText.text = "Customer 4 is angry   >:(";
    }

    void UICustomer5Angry()
    {
        isActive = true;
        Popup.enabled = true;
        popText.enabled = true;
        popText.text = "Customer 5 is angry   >:(";
    }

    void UICustomerSuccess()
    {
        isActive = true;
        Popup.enabled = true;
        popText.enabled = true;
        popText.text = "Success!";
    }

    void UIPlateTooMany()
    {
        isActive = true;
        Popup.enabled = true;
        popText.enabled = true;
        popText.text = "You're carrying too many vegetables!";
    }

    void UIEndGameP1()
    {
        EndGameCanvas.enabled = true;
        Text winner = GameObject.Find("EndGameCanvas/Winning Player").GetComponent<Text>();
        winner.text = "PLAYER 1 WINS!";
        Text winnerScore = GameObject.Find("EndGameCanvas/Winning Player Score").GetComponent<Text>();
        int topScore = PlayerControl.control.score;
        winnerScore.text = topScore.ToString();
        List<int> highScores = PlayerControl.control.highScores;
        string rString = "High Scores\n";
        for (int i = 0; i < highScores.Count; i++)
        {
            string s = highScores[i].ToString();
            rString += s + "\n";
        }
        Text hs = GameObject.Find("EndGameCanvas/HighScores").GetComponent<Text>();
        hs.text = rString;
        Time.timeScale = 0;
    }

    void UIEndGameP2()
    {
        EndGameCanvas.enabled = true;
        Text winner = GameObject.Find("EndGameCanvas/Winning Player").GetComponent<Text>();
        winner.text = "PLAYER 2 WINS!";
        Text winnerScore = GameObject.Find("EndGameCanvas/Winning Player Score").GetComponent<Text>();
        int topScore = PlayerControl.control.score;
        winnerScore.text = topScore.ToString();
        List<int> highScores = PlayerControl.control.highScores;
        string rString = "High Scores\n";
        for (int i = 0; i < highScores.Count; i++)
        {
            string s = highScores[i].ToString();
            rString += s + "\n";
        }
        Text hs = GameObject.Find("EndGameCanvas/HighScores").GetComponent<Text>();
        hs.text = rString;
        Time.timeScale = 0;
    }

    private void LateUpdate()
    {
        if (isActive)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                Popup.enabled = false;
                popText.enabled = false;
                isActive = false;
                time = 3;
            }
        }
    }


}
