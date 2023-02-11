using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSceneButton : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("TF");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
