using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Navigation : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Base");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
