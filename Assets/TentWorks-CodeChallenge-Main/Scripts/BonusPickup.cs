using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPickup : MonoBehaviour
{
    public string playerName;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;
        PlayerActivity pa = player.GetComponent<PlayerActivity>();
        string bonus = gameObject.name;

       if (pa.name == playerName)
        {
            //Apply spawned bonus
            switch (bonus)
            {
                case "Score":
                    pa.score += 10;
                    NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
                    break;
                case "Speed":
                    PlayerMovement pm = player.GetComponent<PlayerMovement>();
                    pm.bonusActive = true;
                    pm.movementSpeed = 6;
                        break;
                case "Time":
                    pa.time += 25;
                        break;
            }
            Destroy(gameObject);
        }

    }
}
