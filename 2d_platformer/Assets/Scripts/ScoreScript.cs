using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class ScoreScript : MonoBehaviour
{
    public static int scoreValue;
    Text score;
    public string name;
    
    // Start is called before the first frame update
    void Start()
    {
        name = PlayerPrefs.GetString("Name");
        var path = Application.persistentDataPath + "//" + name + ".txt";
        string json = File.ReadAllText(path);
        Player player = JsonUtility.FromJson<Player>(json);
        scoreValue = player.score;
        score = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreValue;
        PlayerPrefs.SetString("Score", scoreValue.ToString());
        /*if (Input.GetKeyDown(KeyCode.F))
            scoreValue += 10;*/
    }

    
}
