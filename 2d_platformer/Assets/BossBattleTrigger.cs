using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Diagnostics;

public class BossBattleTrigger : MonoBehaviour
{
    public Camera main;  //main player camera
    public Camera boss;  //camera for the boss
    public AIPath path;
    public Animator animator;
    public Stopwatch myTimer = new Stopwatch();
    public GameObject bossCam;
    public Rigidbody2D rb; 

    public bool hasBeenEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();  //get the animator component of the boss
        //rb = GetComponent<Rigidbody2D>();     //get the rigidbody of the boss
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (myTimer.ElapsedMilliseconds >= 2000)   //wait 2.0 seconds for the initial part of the boss
        {
            rb.gravityScale = 1;   //set the boss' gravity so he falls
        }

        if (myTimer.ElapsedMilliseconds >= 3000)
        {
            animator.SetTrigger("IsActivated"); //trigger for his flying animation
            rb.gravityScale = 0;            //stop his fall part of the intro, first we stop gravity
            
            rb.velocity = Vector3.zero;     // then set his velocity to 0
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && hasBeenEnabled == false)
        {
            hasBeenEnabled = true;     //sentinel flag here so that it doesn't keep running this if statement while the player is in the collider
            bossCam.SetActive(true);  //enable the camera since it is not set active by default
            boss.enabled = true;  //enable the view to see the boss cam
            main.enabled = false;  //disable main camera for small scene
            myTimer.Start();  //start the timer that makes the boss wait in the scene for a moment before attacking

            
        }   
            //Vector3 gravity = new Vector3(1, 1, 1);     //turn on the gravity in the AIPath script
            //path.gravity = gravity;     //set the new gravity
            
    }

    /// <summary>
    /// enable the boss
    /// </summary>
    public void enableBoss()
    {
        
    }
}
