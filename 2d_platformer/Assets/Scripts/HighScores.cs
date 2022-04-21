using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HighScores : MonoBehaviour
{
    List<HighScoreEntry> scores = new List<HighScoreEntry>();
    static Text HighScore;

    void Start()
    {

        DirectoryInfo di = new DirectoryInfo(@Application.persistentDataPath + Path.AltDirectorySeparatorChar);
        FileInfo[] files = di.GetFiles("*.txt");

        foreach (FileInfo file in files)
        {
            string json = File.ReadAllText(file.FullName);
            Player player = JsonUtility.FromJson<Player>(json);
            scores.Add(new HighScoreEntry { name = player.name.ToString(), score = player.score });
        }

        scores.Sort((HighScoreEntry x, HighScoreEntry y) => y.score.CompareTo(x.score));

        HighScore = GetComponent<UnityEngine.UI.Text>();

        for (int i = 0; i < 5; i++)
        {
            HighScore.text = HighScore.text + scores[i].name + ": " + scores[i].score + "\n";
        }
    }

}