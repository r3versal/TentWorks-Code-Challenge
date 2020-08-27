///-----------------------------------------------------------------
///   Class:          HighScoreManager
///   Description:    This script is the middle man between game data and PlayerControl(file) data, the methods are broken up to avoid threading issues(ie threads that are still open before next interaction)
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
#region using directives
using System.Collections.Generic;
using UnityEngine;
#endregion

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
        PlayerControl.control.LoadAll();

        NotificationCenter.DefaultCenter.AddObserver(this, "Savep1Data");
        NotificationCenter.DefaultCenter.AddObserver(this, "Savep2Data");
    }

    void Savep1Data()
    {
        PlayerActivity go1 = GameObject.Find("Player1").GetComponent<PlayerActivity>();
        if (go1.score > 0)
        {
            p1Score = go1.score;
        }
        else
        {
            p1Score = 0;
        }
        p1ReadyToSave = true;
        if (p2ReadyToSave)
        {
            saveEverything();
        }
    }

    void Savep2Data()
    {
        PlayerActivity go2 = GameObject.Find("Player2").GetComponent<PlayerActivity>();
        if(go2.score > 0)
        {
            p2Score = go2.score;
        }
        else
        {
            p2Score = 0;
        }
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
