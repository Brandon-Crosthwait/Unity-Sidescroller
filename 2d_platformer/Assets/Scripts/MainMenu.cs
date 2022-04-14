using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    SaveLoad saveload = new SaveLoad();
    public InputField iField;

    //private float masterVolume = 0;
    public void PlayGame()
    {
        PlayerPrefs.SetString("PreviousLevel", "");
        SceneManager.LoadScene(Build.sceneOrder.Credentials.ToString());
    }

    public void SetName()
    {
        PlayerPrefs.SetString("Name", iField.text);
        PlayerPrefs.SetString("PreviousLevel", "");
        //saveload.CreatePlayer(iField.text);
        SceneManager.LoadScene(Build.sceneOrder.LevelSelect.ToString());
    }

    public void LoadGame()
    {
        Debug.Log("LOADED GAME");
    }

    public void saveGame()
    {
        saveload.SavePlayer();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
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
