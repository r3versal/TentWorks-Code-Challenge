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
        p1Score = 74;
        p1ReadyToSave = true;
        if (p2ReadyToSave)
        {
            saveEverything();
        }
    }

    void Savep2Data()
    {
        p2Score = 34;
        p2ReadyToSave = true;
        if (p1ReadyToSave)
        {
            saveEverything();
        }
    }

    void saveEverything()
    {
        if (!oneTime)
        {
            oneTime = true;
            //Only the winner gets to save high score
            int maxNum = Mathf.Max(p1Score, p2Score);
            PlayerControl.control.score = maxNum;
            PlayerControl.control.SaveAll();
            p1ReadyToSave = false;
            p2ReadyToSave = false;
        }

    }
}
