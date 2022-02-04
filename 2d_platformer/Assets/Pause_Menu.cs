using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Pause_Menu : MonoBehaviour
{
    //GameObjects
    public GameObject pauseMenuUI;
    public GameObject pauseMenuSubMenu;
    public GameObject settingsMenuSubMenu;

    public void ResumeGame()
    {
        ResetPauseVariables();
    }

    //Return to Main Menu
    public void MainMenu()
    {
        ResetPauseVariables();
        SceneManager.LoadScene(Build.sceneOrder.MainMenu.ToString());
    }

    //Go To Settings Menu
    public void Settings()
    {
        pauseMenuSubMenu.SetActive(false);
        settingsMenuSubMenu.SetActive(true);
    }

    //Helper Method to remove all pause/time restraints from game
    public void ResetPauseVariables()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        PlayerMovement.isPaused = false;
        UIManager.gameIsPaused = false;
    }

}
