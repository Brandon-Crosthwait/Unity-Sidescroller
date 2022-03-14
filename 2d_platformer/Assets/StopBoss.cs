using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class StopBoss : MonoBehaviour
{
    public AIPath Bat1;
    public AIPath Bat2;
    public AIPath Bat3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Scene currentScene;
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "BossFight 2" && collision.tag == "Player")
        {
            Bat1.canMove = false;
            Bat2.canMove = false;
            Bat3.canMove = false;
        }
    }
}
