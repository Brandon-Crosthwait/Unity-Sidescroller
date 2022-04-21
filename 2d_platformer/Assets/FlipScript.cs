using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlipScript : MonoBehaviour
{
    public AIPath aiPath;   //AIPath script reference
    public SpriteRenderer renderer;

    public Animator animator;

    public Health playerHealth;

    

    // Start is called before the first frame update
    void Start()
    { 
        renderer = transform.GetComponent<SpriteRenderer>();
        renderer.flipX = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)     //if his x movement is positive, face right
        {
            renderer.flipX = false;
           //transform.localScale = new Vector3(-17, 17, 1);  //change localScale to have him face right

        }
        else if (aiPath.desiredVelocity.x <= -0.01f)  //if his speed is moving left
        {
            renderer.flipX = true;
            //transform.localScale = new Vector3(17f, 17f, 1f);  //change localScale to have him face Lect
        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player") //&& health.currentHealth > 0)  
        {
            Health health = collision.GetComponent<Health>();   //get the health script

            //bool stopAnimation = false;

            if (health.currentHealth <= 0)  //check if the player has died
            {
              //PlayerMovement pm = collision.GetComponent<PlayerMovement>();
                //pm.canMove = false;
                aiPath.canMove = false;    //if player has died AI stops moving
                //stopAnimation = true;
            }
            else
            {
                if (aiPath.canMove == true)
                {
                    health.TakeDamage(1f);   //take one damage
                }
                
            }
            /* if (health.currentHealth > -1)
            {
                animator.Play("Bat_attack", 0, 0);
            } */

            
            
            
        }

        if (collision.tag == "EndBattle")   //trigger at top floor of bossbattle2 to stop the enemies from flying at the end
        {
            aiPath.maxSpeed = 0;
        }
    }
}
