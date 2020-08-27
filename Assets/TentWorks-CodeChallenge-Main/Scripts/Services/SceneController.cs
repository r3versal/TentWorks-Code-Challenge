///-----------------------------------------------------------------
///   Class:          SceneController
///   Description:    This script is responsible for end of game options
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
#region using directives
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion

public class SceneController : MonoBehaviour
{
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
