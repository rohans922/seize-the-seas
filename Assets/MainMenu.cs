using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Back2()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void Back3()
    {
        SceneManager.LoadScene("HowToPlay2");
    }
    public void Next2()
    {
        SceneManager.LoadScene("HowToPlay2");
    }

    public void Next3()
    {
        SceneManager.LoadScene("HowToPlay3");
    }
}
