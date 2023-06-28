using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButtom;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject gameMenu;
    void Update()
    {
        
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseButtom.SetActive(false);
        pauseMenu.SetActive(true);
        gameMenu.SetActive(false);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        pauseButtom.SetActive(true);
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);
    }
    public void BackToSelectLevelWithoutSaving()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        SceneManager.LoadScene("SelectLevels");
    }
}
