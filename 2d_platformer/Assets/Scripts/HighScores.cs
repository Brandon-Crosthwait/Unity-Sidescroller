using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    List<HighScoreEntry> scores = new List<HighScoreEntry>();
    static Text HighScoresList;

    void Start()
    {
        HighScoresList = GetComponent<Text>();
        // Adds some test data
        AddNewScore("Bill", 10);
        AddNewScore("Brandon", 8);
        AddNewScore("Chris", 6);
        AddNewScore("Boris", 4);
        AddNewScore("Tib", -1);

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        HighScoresList.text = null;
        scores.Sort((HighScoreEntry x, HighScoreEntry y) => y.score.CompareTo(x.score));
        for(int i =0; i <= 5; i++)
        {
            HighScoresList.text += scores[i].name + ": " + scores[i].score + "\n";
        }
        
    }

    public void AddNewScore(string entryName, int entryScore)
    {
        scores.Add(new HighScoreEntry { name = entryName, score = entryScore });
    }
}