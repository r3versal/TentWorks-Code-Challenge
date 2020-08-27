///-----------------------------------------------------------------
///   Class:          BonusHandler
///   Description:    This script is responsible for spawning the bonus for a player if a Customer is served in a timely manner, it is called using the notification system
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
#region using directives
using System.Collections.Generic;
using UnityEngine;
#endregion

public class BonusHandler : MonoBehaviour
{
    private string[] pickups;
    private List<Vector3> pickupLocations;
    private string bonus;
    private string playerName;

    void Start()
    {
        pickupLocations = new List<Vector3>();
        pickupLocations.Add(new Vector3(0, .34f, .95f));
        pickupLocations.Add(new Vector3(0, .34f, -1.48f));
        pickupLocations.Add(new Vector3(-4.35f, .34f, -1.48f));

        pickups = new string[]
        {
                 "Score",
                 "Time",
                 "Speed"
        };

        NotificationCenter.DefaultCenter.AddObserver(this, "SpawnBonus");
        NotificationCenter.DefaultCenter.AddObserver(this, "SpawnBonusP2");
    }

    //Called via Notification Center if Player 1 triggered the bonus
    void SpawnBonus()
    {
        playerName = "Player1";
        int bonusIndex = Random.Range(0, pickups.Length);
        bonus = pickups[bonusIndex];
        int randomInt = Random.Range(0, pickupLocations.Count - 1);

        //Spawn in random location from array of possible vectors
        Vector3 spawnPos = pickupLocations[randomInt];
        GameObject go = Instantiate(Resources.Load("Prefabs/Pickups/Bonus"), spawnPos, Quaternion.identity) as GameObject;
        go.name = bonus;
        BonusPickup bp = go.GetComponent<BonusPickup>();
        bp.playerName = playerName;

        switch (bonus)
        {
            case "Score":
                NotificationCenter.DefaultCenter.PostNotification(this, "UIBonusScore");
                break;
            case "Speed":
                NotificationCenter.DefaultCenter.PostNotification(this, "UIBonusSpeed");
                break;
            case "Time":
                NotificationCenter.DefaultCenter.PostNotification(this, "UIBonusTime");
                break;
        }
    }

    //Called via Notification Center if Player 2 triggered the bonus
    void SpawnBonusP2()
    {
        playerName = "Player2";
        int bonusIndex = Random.Range(0, pickups.Length);
        bonus = pickups[bonusIndex];
        int randomInt = Random.Range(0, pickupLocations.Count - 1);

        //Spawn in random location from array of possible vectors
        Vector3 spawnPos = pickupLocations[randomInt];
        GameObject go = Instantiate(Resources.Load("Prefabs/Pickups/Bonus"), spawnPos, Quaternion.identity) as GameObject;
        go.name = bonus;
        BonusPickup bp = go.GetComponent<BonusPickup>();
        bp.playerName = playerName;

        switch (bonus)
        {
            case "Score":
                NotificationCenter.DefaultCenter.PostNotification(this, "UIBonusScore");
                break;
            case "Speed":
                NotificationCenter.DefaultCenter.PostNotification(this, "UIBonusSpeed");
                break;
            case "Time":
                NotificationCenter.DefaultCenter.PostNotification(this, "UIBonusTime");
                break;
        }
    }
}

