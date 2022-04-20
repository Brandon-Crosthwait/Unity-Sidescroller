using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;

public class SaveLoad
{
    private Player player;
    private Health health;
    private ScoreScript scoreScript;
    private PlayerMovement playerMovement;
    private Timer timer;
    private string path = "";
    private string persistentPath = "";
    

    public void CreatePlayer(string Name) {
        player = new Player();
        player.name = Name;
        player.score = 0;
        player.health = 3;
        player.level = 1;
        player.checkpoint = "false";
        player.minutes = 0.0f;
        player.seconds = 0.0f;
        SaveData();
    }

    public void SavePlayer() {
        //needs to be edited to take playerprefs of score health and timer into account
        player = new Player();
        player.name = PlayerPrefs.GetString("Name");
        player.score = float.Parse(PlayerPrefs.GetString("Score"));
        player.health = float.Parse(PlayerPrefs.GetString("Health"));
        player.level = 1;
        player.checkpoint = PlayerPrefs.GetString("Checkpoint");
        player.minutes = float.Parse(PlayerPrefs.GetString("Minutes"));
        player.seconds = float.Parse(PlayerPrefs.GetString("Seconds"));
        SaveData();
        Debug.Log("Player Saved");
    }

    public void SetPaths() {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + PlayerPrefs.GetString("Name") + ".json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + PlayerPrefs.GetString("Name") + ".json";
    }

    public void SaveData() {
        var path = Application.persistentDataPath + "//" + player.name + ".txt";
        string json = JsonUtility.ToJson(player);

        File.WriteAllText(path, json);
    }

    public void LoadData(string Name) {
        var path = Application.persistentDataPath + "//" + Name + ".txt";
        string json = File.ReadAllText(path);
        player = JsonUtility.FromJson<Player>(json);
        Debug.Log(player);
    }

}
