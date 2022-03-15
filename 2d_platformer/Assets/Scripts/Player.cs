using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    private static int id;
    public string name;
    public float score;
    public float health;
    public int level;
    public float timer;
    public bool checkpoint;
}