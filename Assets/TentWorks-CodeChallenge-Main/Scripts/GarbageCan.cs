using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
        pa.veg1.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        pa.veg2.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");

        pa.score -= 10;
        NotificationCenter.DefaultCenter.PostNotification(this, "UpdateScore");
    }
}
