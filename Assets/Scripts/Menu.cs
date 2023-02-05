using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public bool Paused = false;
    public void Launch()
    {
        SceneManager.LoadScene("TF");

    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Paused = true;
    }
    public void Pause()
    {
        gameObject.SetActive(true);
        Paused = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void GoBackToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
