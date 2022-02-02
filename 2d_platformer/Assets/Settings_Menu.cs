using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Settings_Menu : MonoBehaviour
{
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    private float volume = 0;
    public GameObject pauseMenuSubMenu;
    public GameObject settingsMenuSubMenu;
    public GameObject volumeDisplayText;
    //private float masterVolume = 0;

    public void ReturnToMainMenu()
    {
        pauseMenuSubMenu.SetActive(true);
        settingsMenuSubMenu.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        this.volume = (float)Math.Round((double)volume,2);
        int volumePercentage = (int)(this.volume * 100);
        volumeDisplayText.GetComponentInChildren<TextMeshProUGUI>().text = volumePercentage.ToString() + "%";

    }

    public void ApplyVolume()
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        
    }
}
