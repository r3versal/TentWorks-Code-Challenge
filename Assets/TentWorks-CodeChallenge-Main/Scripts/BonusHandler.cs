using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHandler : MonoBehaviour
{
    private string[] pickups;
    private List<Vector3> pickupLocations;
    private string bonus;
    public string playerName;

    void Start()
    {
        pickupLocations = new List<Vector3>();
        pickupLocations.Add(new Vector3(0, .34f, 5));
        pickupLocations.Add(new Vector3(2, .34f, 5));
        pickupLocations.Add(new Vector3(5, .34f, 5));

        pickups = new string[]
        {
                 "Score",
                 "Time",
                 "Speed"
        };

        NotificationCenter.DefaultCenter.AddObserver(this, "SpawnBonus");
    }

    void SpawnBonus()
    {
        int bonusIndex = Random.Range(0, pickups.Length);
        bonus = pickups[bonusIndex];
        int randomInt = Random.Range(0, pickupLocations.Count - 1);

        //Spawn in random location from array of possible vectors
        Vector3 spawnPos = pickupLocations[randomInt];
        GameObject go = Instantiate(Resources.Load("Prefabs/Pickups/" + bonus), spawnPos, Quaternion.identity) as GameObject;
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

