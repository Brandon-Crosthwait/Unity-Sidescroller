using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NameMenu : MonoBehaviour
{

    public void PlayGame()
    {
        
        PlayerPrefs.SetString("PreviousLevel", "");
        SceneManager.LoadScene(Build.sceneOrder.LevelSelect.ToString()); //Assuming Level Select stays at build index 3
    }
}
