using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButtom;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject gameMenu;

    private bool pausedGame = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausedGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        pausedGame = true;
        Time.timeScale = 0f;
        pauseButtom.SetActive(false);
        pauseMenu.SetActive(true);
        gameMenu.SetActive(false);
    }
    public void Resume()
    {
        pausedGame = false;
        Time.timeScale = 1f;
        pauseButtom.SetActive(true);
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);
    }
    public void Restart()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        AttackController2 attackController = FindAnyObjectByType<AttackController2>();
        attackController.ResetSceneFromMainMenu();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void BackToSelectLevelWithoutSaving()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        SceneManager.LoadScene("SelectLevels");
    }
    public void BackToMainMenuWithoutSaving()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        SceneManager.LoadScene("MainMenu");
    }
}
