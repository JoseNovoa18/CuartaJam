using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SelectCharactersScene()
    {
        SceneManager.LoadScene("LevelPrototype");
    }

    public void TestGameScene()
    {
        SceneManager.LoadScene("LevelPrototype");
    }

    public void Quit()
    {
        Debug.Log("Salio del juego");
        Application.Quit();
    }
}
