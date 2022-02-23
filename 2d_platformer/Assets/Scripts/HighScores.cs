using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    //List<HighScoreEntry> scores = new List<HighScoreEntry>();
    static Text HighScore;
    string score;

    void Start()
    {
        HighScore = GetComponent<UnityEngine.UI.Text>();
        score = PlayerPrefs.GetString("highscore");
        HighScore.text = score;
    }

}