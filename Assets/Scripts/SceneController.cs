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

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SecondMainMenuScene()
    {
        SceneManager.LoadScene("SecondMainMenu");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void PrototypeFinal()
    {
        CountBrains.Instance.SaveBrains();
        SceneManager.LoadScene("PracticeLevelPrototypeFinal");
    }

    public void PrototypeFinalLIZETH()
    {
        CountBrains.Instance.SaveBrains();
        SceneManager.LoadScene("PracticeLevelPrototypeLIZETH");
    }

    public void Quit()
    {
        Debug.Log("Salio del juego");
        Application.Quit();
    }
    public void Restart()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        AttackController2 attackController = FindAnyObjectByType<AttackController2>();
        attackController.ResetSceneFromMainMenu();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void MainMenuWithoutSaving()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        SceneManager.LoadScene("MainMenu");
    }
    public void SelectLevelWithoutSaving()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        SceneManager.LoadScene("SelectLevels");
    }
    public void BackToMainMenuWithoutSaving()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        SceneManager.LoadScene("MainMenu");
        
        AttackController2 attackController = FindAnyObjectByType<AttackController2>();
        attackController.ResetSceneFromMainMenu();
        
    }

    public void BackToSelectLevelWithoutSaving()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        SceneManager.LoadScene("SelectLevels");
        
        AttackController2 attackController = FindAnyObjectByType<AttackController2>();
        attackController.ResetSceneFromMainMenu();
        
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