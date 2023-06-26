using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectLevels");
    }

    public void Level1()
    {
        SceneManager.LoadScene("PracticeLevelPrototype");
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void Quit()
    {
        Debug.Log("Salio del juego");
        Application.Quit();
    }
}
