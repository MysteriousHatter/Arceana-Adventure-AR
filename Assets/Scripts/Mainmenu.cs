using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Mainmenu : MonoBehaviour
{
    public void PlayStartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayMain()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayHistory()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayResultScene()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
