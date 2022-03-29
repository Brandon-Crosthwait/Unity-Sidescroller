using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Status : MonoBehaviour
{
    

    public int health = 20;   //health of the boss
    public GameObject end;   //end trophy
    public Animator animator; //animator for the boss
    public Bossman_run br;  //get reference to script so we can set canMove to false when boss is defeated
    public Stopwatch animationTimer = new Stopwatch(); //timer to delete the boss gameobject once he has hit defeated status
    public ScoreScript scoreScript;  //get the scorescript to add up score for the boss
    public AudioSource bossDeathSound; //sound when the boss is defeated
    bool isDefeated = false;  //bool to store if the boss is defeated

    void Update()
    {
        if (animationTimer.IsRunning)
        {

            if (animationTimer.ElapsedMilliseconds >= 1400)
            {
                Destroy(gameObject);  //delete the boss after half a second has passed
                animationTimer.Stop(); //stop the timer
                end.SetActive(true);  //make the end available
            }
        }
    }

    /// <summary>
    /// deplete the boss health if we hit him with the projectile
    /// </summary>
    public void DepleteHealth()
    {
        health--;
        Scene m_scene;
        m_scene = SceneManager.GetActiveScene();
        /*
        if (m_scene.name == "BossFight 2")
        {
            animator.SetTrigger("isDefeated");
            br.canMove = false;
            animationTimer.Start();
            ScoreScript.scoreValue = +50;
            bossDeathSound.Play();
            isDefeated = true;
            //Destroy(gameObject);  //delete the boss
        }
        */

         if (health <= 0 && isDefeated == false && m_scene.name != "BossFight 2")
        {
            animator.SetTrigger("isDefeated");
            br.canMove = false;
            animationTimer.Start();
            ScoreScript.scoreValue = +50;
            bossDeathSound.Play();
            isDefeated = true;
            //Destroy(gameObject);  //delete the boss
        }
        
    }
}
