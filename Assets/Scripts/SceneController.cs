using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class SceneController : MonoBehaviour
{
    [SerializeField] private int brains;
    public TextMeshProUGUI[] brainsTextArray;

    private void Start()
    {
        brains = CountBrains.Instance.Brainss;
        UpdateBrainsText();
    }
    private void UpdateBrainsText()
    {
        string brainsString = brains.ToString();
        foreach (TextMeshProUGUI textMeshProUGUI in brainsTextArray)
        {
            textMeshProUGUI.text = brainsString;
        }
    }
    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectLevels");
    }

    public void Level1()
    {
        CountBrains.Instance.SaveBrains();
        SceneManager.LoadScene("Level1");
    }
    public void Level2()
    {
        CountBrains.Instance.SaveBrains();
        SceneManager.LoadScene("Level2");
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PrototypeFinal()
    {
        CountBrains.Instance.SaveBrains();
        SceneManager.LoadScene("PracticeLevelPrototypeFinal");
    }

    public void Quit()
    {
        Debug.Log("Salio del juego");
        Application.Quit();
    }

    public void BackToSelectLevelWithoutSaving()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        SceneManager.LoadScene("SelectLevels");
    }
    //Antes
    /*    
    public void SelectLevel()
    {
        SceneManager.LoadScene("PracticeLevelPrototypeFinal");
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

    public void BackToSelectLevelWithoutSaving()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        SceneManager.LoadScene("MainMenu");
    }
    */

}