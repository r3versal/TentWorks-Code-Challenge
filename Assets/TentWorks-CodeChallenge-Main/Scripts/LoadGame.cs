using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    private GameObject Player1;
    private GameObject Player2;

    private Material matP1;
    private Material matP2;

    private void Awake()
    {
        //Instantiate players
        Player1 = Instantiate(Resources.Load("Prefabs/Player1"), new Vector3(-6f, .61f, -2.7f), Quaternion.identity) as GameObject;
        Player2 = Instantiate(Resources.Load("Prefabs/Player2"), new Vector3(6f, .61f, -2.7f), Quaternion.identity) as GameObject;
    }
    void Start()
    {
        
    }

}
