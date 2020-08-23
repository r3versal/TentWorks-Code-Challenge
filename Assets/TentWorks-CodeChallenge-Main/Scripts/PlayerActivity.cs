using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActivity : MonoBehaviour
{
    private Text playerTimeText;
    private Text playerScoreText;
    private string playerNum;
    
    public string name;
    public float score;
    public float time ;

    public List<string> vegetables;

    private void Awake()
    {
        playerNum = gameObject.name.Substring(0, 7);
        if (name == null)
        {
            name = playerNum;
        }
    }
    void Start()
    {
        if(playerNum == "Player1")
        {
            //assign text for time and score
            playerTimeText = GameObject.Find("Canvas/Player1 Stats/Timer").GetComponent<Text>();
            playerScoreText = GameObject.Find("Canvas/Player1 Stats/Score").GetComponent<Text>();
        }
        else
        {
            //assign text for time and score
            playerTimeText = GameObject.Find("Canvas/Player2 Stats/Timer").GetComponent<Text>();
            playerScoreText = GameObject.Find("Canvas/Player2 Stats/Score").GetComponent<Text>();
        }
        score = 0;
        time = 100;

        playerScoreText.text = "Score : " + score;

        vegetables = new List<string>();

        //TODO add notification observer that time has changed via pickup and needs to be updated
        //TODO add notification observer that score has changed and needs to be updated
    }

    private void OnCollisionEnter(Collision collision)
    {
     GameObject go = collision.gameObject;
        if(go.tag == "Vegetable")
        {
            if(vegetables.Count == 2)
            {
                //TODO UI saying too many vegetables! Use garbage can!
            }
            else
            {
                vegetables.Add(go.name);
            }
            Debug.Log(go.name);
        }

    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            string seconds = (time % 60).ToString("00");
            playerTimeText.text = "Time : " + seconds;
           
        }
    }
}
