using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bossman_run : MonoBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 7f;
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
    public GameObject Left;
    public GameObject Right;
    public Stopwatch time = new Stopwatch();
    public GameObject head;
    public AudioSource attack;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        boss = gameObject.GetComponent<Boss>();
        animator = rb.GetComponent<Animator>();
        pm = player.GetComponent<PlayerMovement>();

        if(SceneManager.GetActiveScene().name == "BossFight 2")  //check so the boss isn't stuck in the rolling animation in later levels
        {
            animator.SetTrigger("Boss_Has_Fallen");
        }

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
            
            if (dist <= 15f)    //boss will only chase if the player's distance is less than 15m
            {

            if (canMove && animator.GetBool("CanAttack") != true)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);  //get a vector from the boss to the player
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos); //move the boss
                animator.SetBool("Boss_speed", true);  //set the animation float value and pass in the player's speed

                if (Vector2.Distance(player.position, rb.position) <= attackRange && player.position.y <= rb.position.y)
                {
                    if (animator.GetBool("CanAttack") == false)
                    {
                        animator.SetBool("CanAttack", true);
                        time.Start();
                        

                    }
                    
                }
                else
                {

                    animator.SetBool("CanAttack", false);

                }


            }
            if (playerRB.gravityScale == 0)
            {
                animator.SetBool("Boss_speed", false);  //set the animation float value and pass in the player's speed
                canMove = false;
                animator.SetBool("CanAttack", false);
            }
            }
            else
            {

            animator.SetBool("CanAttack", false);

            }


        if (time.IsRunning)
        {
            if (time.ElapsedMilliseconds >= 400 && time.ElapsedMilliseconds <= 700)
            {
                if (player.position.x < rb.position.x)
                {
                    Left.SetActive(true);
                    Right.SetActive(false);
                }
                if (player.position.x > rb.position.x)
                {
                    Left.SetActive(false);
                    Right.SetActive(true);
                }
            }

            if (time.ElapsedMilliseconds >= 1000)
            {
                animator.SetBool("CanAttack", false);
                Left.SetActive(false);
                Right.SetActive(false);
                time.Stop();
                time.Reset();
            }
        }

            
        
           
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player") //check if the collision is the player
        {
            if (playerRB.position.y + 0.5 >= rb.position.y)
            {


                //Vector2 knockback = new Vector2(0f, 10f);
                //playerRB.AddForce(knockback);
            }
            else
            {
                //isHit = true;
                Health health = collision.GetComponent<Health>();   //get the health script
                health.TakeDamage(1f);                              //call the method
                                                                    //pm.SetCanMove(false);
                //Vector2 bounce = new Vector2(20f, 0f);
                //bossRB.AddForce(bounce);


            }



        }

    }
}
