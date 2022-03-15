using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    private Player player;
    private Health health;
    private ScoreScript scoreScript;
    private PlayerMovement playerMovement;
    private Timer timer;
    private string path = "";
    private string persistentPath = "";
    // Start is called before the first frame update
    void Start()
    {  
        CreatePlayer();
        SetPaths();        
    }

    private void CreatePlayer() {
        player = new Player();
        player.name = PlayerPrefs.GetString("Name");
        player.score = ScoreScript.scoreValue;
        player.health = health.currentHealth;
        player.level = 1;
        player.timer = Timer.FlowingTime;
    }

    public void SetPaths() {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + PlayerPrefs.GetString("Name") + ".json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + PlayerPrefs.GetString("Name") + ".json";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SaveData();
        }
    }

    public void SaveData() {
        string savePath = path;
        Debug.Log("Saving data at: " + savePath);
        string json = JsonUtility.ToJson(player);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }
}
