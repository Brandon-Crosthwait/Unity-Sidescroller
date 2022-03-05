using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Player
{
    private static int id = 0;
    public string name;
    public float score;
    public float health;
    public int level;
    public int checkpoint;

    public Player(string name, float score, float health, int level, int checkpoint)
    {
        id += 1;
        this.name = name;
        this.score = score;
        this.health = health;
        this.level = level;
        this.checkpoint = checkpoint;
    } 
}