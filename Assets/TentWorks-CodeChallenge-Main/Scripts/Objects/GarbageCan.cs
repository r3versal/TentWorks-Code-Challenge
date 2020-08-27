///-----------------------------------------------------------------
///   Class:          GarbageCan
///   Description:    This script is attached to the garbage can object and handles player collisions
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
#region using directives
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class GarbageCan : MonoBehaviour
{
    public List<string> vegetablesToDispose;

    public Image veg1;
    public Image veg2;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;
        PlayerActivity pa = player.GetComponent<PlayerActivity>();
        vegetablesToDispose = pa.vegetables;
        pa.vegetables = new List<string>();
        pa.veg1.sprite = Resources.Load<Sprite>("Sprites/emptySprite");
        pa.veg2.sprite = Resources.Load<Sprite>("Sprites/emptySprite");

        pa.score -= 10;
        NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
    }
}
