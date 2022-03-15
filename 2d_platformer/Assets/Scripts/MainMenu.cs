using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    public InputField iField;

    //private float masterVolume = 0;
    public void PlayGame()
    {
        PlayerPrefs.SetString("PreviousLevel", "");
        SceneManager.LoadScene(Build.sceneOrder.Credentials.ToString()); //Assuming Level Select stays at build index 3
    }

    public void SetName()
    {
        /*
        Player newPlayer = new Player();
        newPlayer.name = iField.text;
        newPlayer.score = 0;
        newPlayer.health = 3;
        newPlayer.level = 1;
        newPlayer.checkpoint = false;

        System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Player));

        var path = Application.dataPath + "//Scripts//Players//" + iField.text + ".xml";
        System.IO.FileStream file = System.IO.File.Create(path);

        writer.Serialize(file, newPlayer);
        file.Close();
        */

        PlayerPrefs.SetString("Name", iField.text);

        PlayerPrefs.SetString("PreviousLevel", "");
        SceneManager.LoadScene(Build.sceneOrder.LevelSelect.ToString());
    }

    public void LoadGame()
    {
        Debug.Log("LOADED GAME");
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
