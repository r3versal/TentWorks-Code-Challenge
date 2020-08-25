using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Canvas endGameCanvas;
    public void RestartScene()
    {
        endGameCanvas.enabled = false;
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {

        Application.Quit();
    }
}
