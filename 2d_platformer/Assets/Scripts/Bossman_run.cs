using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bossman_run : MonoBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 3f;
    public GameObject hb;
    public Transform player;
    public Rigidbody2D rb;
    Boss boss;
    Animator m_Animator;
    Damage damage;
    public GameObject bossGO;
    public PlayerMovement pm;

    public Animator animator;
    public Rigidbody2D playerRB;
    public bool canMove = true;
    public Camera mainCamera;
    public Camera bossCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        boss = gameObject.GetComponent<Boss>();
        animator = rb.GetComponent<Animator>();
        pm = player.GetComponent<PlayerMovement>();

        //mainCamera.enabled = true;
        //bossCamera.enabled = false;

    }

    public void cameraTimer()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boss.LookAtPlayer();  //calls other script and flips the boss around

            float dist = Vector3.Distance(player.position, rb.position);  //get distance between boss and player
            
            if (dist <= 10f)    //boss will only chase if the player's distance is less than 10m
            {

            if (canMove)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);  //get a vector from the boss to the player
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos); //move the boss
                animator.SetBool("Boss_speed", true);  //set the animation float value and pass in the player's speed

            }
            if (playerRB.gravityScale == 0)
            {
                animator.SetBool("Boss_speed", false);  //set the animation float value and pass in the player's speed
                canMove = false;

            }
            }
        else
        {

        }

            
        
           
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player") //check if the collision is the player
        {
            //isHit = true;
            Health health = collision.GetComponent<Health>();   //get the health script
            health.TakeDamage(1f);                              //call the method
            //pm.SetCanMove(false);
            Vector2 bounce = new Vector2(20f, 0f);
            //bossRB.AddForce(bounce);



        }

    }
}
