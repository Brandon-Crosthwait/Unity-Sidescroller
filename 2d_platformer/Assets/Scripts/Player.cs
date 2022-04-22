using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    private static int id;
    public string name;
    public int score;
    public float health;
    public int level;
    public float minutes;
    public float seconds;
    public string checkpoint;
    public int characterAnimatorOverriderID;
}