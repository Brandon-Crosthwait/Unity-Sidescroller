using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Pause_Menu : MonoBehaviour
{
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    public GameObject pauseMenuUI;
    public GameObject pauseMenuSubMenu;
    public GameObject settingsMenuSubMenu;
    //private float masterVolume = 0;
    public void ResumeGame()
    {
        ResetPauseVariables();
    }

    public void MainMenu()
    {
        ResetPauseVariables();
        SceneManager.LoadScene(0); //this is assuming main menu stays at build index 0
    }

    public void Settings()
    {
        pauseMenuSubMenu.SetActive(false);
        settingsMenuSubMenu.SetActive(true);
    }

    public void ResetPauseVariables()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        PlayerMovement.isPaused = false;
        UIManager.gameIsPaused = false;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void ApplyVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void LoadVolume()
    {
        //masterVolume = PlayerPrefs.GetFloat("masterVolume");
        volumeTextValue.text = PlayerPrefs.GetFloat("masterVolume").ToString("F1");
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
    }
}
