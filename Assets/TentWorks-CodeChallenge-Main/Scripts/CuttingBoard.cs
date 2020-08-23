using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    public List<string> vegetablesToChop;
    public List<string> vegetablesChopped;


    // Start is called before the first frame update
    void Start()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer1Ended");
        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer2Ended");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;
        PlayerActivity pa = player.GetComponent<PlayerActivity>();
        vegetablesToChop = pa.vegetables;
        pa.vegetables = new List<string>();

        foreach(string s in vegetablesToChop)
        {
            Debug.Log("Veg List: "+ s);
        }
        if (gameObject.name == "CuttingBoard1")
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "ChopTimer1Start");
        }

        else
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "ChopTimer2Start");
        }

        if (vegetablesToChop.Count == 2)
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "IsTwo");
        }


    }
}
