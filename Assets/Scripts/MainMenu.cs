using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playEasy()
    {
        SceneManager.LoadScene("Easy Mode");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
