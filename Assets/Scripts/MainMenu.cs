using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("SelectCharacters");
    }

    public void Quit()
    {
        Debug.Log("Salio del juego");
        Application.Quit();
    }
}
