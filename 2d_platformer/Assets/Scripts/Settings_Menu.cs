using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Settings_Menu : MonoBehaviour
{
    //Volume Controls
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    //Floats
    private float volume = 0;

    //Gameobjects
    public GameObject pauseMenuSubMenu;
    public GameObject settingsMenuSubMenu;
    public GameObject volumeDisplayText;

    //Go To Main Menu
    public void ReturnToMainMenu()
    {
        pauseMenuSubMenu.SetActive(true);
        settingsMenuSubMenu.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        this.volume = (float)Math.Round((double)volume,2); //Calculate Volume as %
        int volumePercentage = (int)(this.volume * 100);
        volumeDisplayText.GetComponentInChildren<TextMeshProUGUI>().text = volumePercentage.ToString() + "%"; //Display Volume as %

    }

    public void ApplyVolume()
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        
    }
}
