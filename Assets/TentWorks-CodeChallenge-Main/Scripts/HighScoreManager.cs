using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public int p1Score;
    public int p2Score;

    private bool p1ReadyToSave;
    private bool p2ReadyToSave;

    private bool oneTime;

    void Start()
    {
        oneTime = false;
        //PlayerControl.control.CreateDataFile();
        PlayerControl.control.LoadAll();
        //update UI with highscores
        List<int> temp = PlayerControl.control.highScores;

        NotificationCenter.DefaultCenter.AddObserver(this, "Savep1Data");
        NotificationCenter.DefaultCenter.AddObserver(this, "Savep2Data");
    }

    void Savep1Data()
    {
        PlayerActivity go1 = GameObject.Find("Player1").GetComponent<PlayerActivity>();
        p1Score = go1.score;
        p1ReadyToSave = true;
        if (p2ReadyToSave)
        {
            saveEverything();
        }
    }

    void Savep2Data()
    {
        PlayerActivity go2 = GameObject.Find("Player2").GetComponent<PlayerActivity>();
        p2Score = go2.score;
        p2ReadyToSave = true;
        if (p1ReadyToSave)
        {
            saveEverything();
        }
    }

    void saveEverything()
    {
        //Make sure this NEVER runs twice
        if (!oneTime)
        {
            oneTime = true;
            //Only the winner gets to save high score
            int maxNum = Mathf.Max(p1Score, p2Score);
            PlayerControl.control.score = maxNum;
            PlayerControl.control.SaveAll();
            p1ReadyToSave = false;
            p2ReadyToSave = false;

            if(maxNum == p1Score)
            {
                NotificationCenter.DefaultCenter.PostNotification(this, "UIEndGameP1");
            }
            else
            {
                NotificationCenter.DefaultCenter.PostNotification(this, "UIEndGameP2");
            }
        }

    }
}
