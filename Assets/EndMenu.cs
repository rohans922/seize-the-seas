using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void PlayAgain()     {         SceneManager.LoadScene("Menu");     }

    public void QuitGame()     {         Application.Quit();     }
}
