///-----------------------------------------------------------------
///   Class:          LoadGame
///   Description:    This script is attached to the Singleton game object and runs only once when the game is loaded
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    private GameObject Player1;
    private GameObject Player2;

    private void Awake()
    {
        //Instantiate players
        Player1 = Instantiate(Resources.Load("Prefabs/Player1"), new Vector3(-5f, .6f, -1.95f), Quaternion.identity) as GameObject;
        Player2 = Instantiate(Resources.Load("Prefabs/Player2"), new Vector3(5f, .6f, -1.95f), Quaternion.identity) as GameObject;
    }

}
