using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static float speed = 20f;
    public Rigidbody2D rb;
    public Transform player;
    public SpriteRenderer bulletSprite;
    public Stopwatch myTimer = new Stopwatch();
    public Boss_Status status;
    public Transform boss;
    

    // Start is called before the first frame update
    void Start()
    {
        BulletHelper bh = new BulletHelper();  //instantiate the helper class

        bool isFacingRight = true;
        float localScale = player.localScale.x;

        bh.checkDirection(ref localScale, ref isFacingRight);
        
        

        

        //check if player is facing right
        if (isFacingRight)
        {
            rb.velocity = transform.right * speed;  //launch the projectile right
            bulletSprite.flipX = false;             //don't flip the bullet spawn point since the player is facing right
        }
        if (!isFacingRight)              //check if the player is facing left
        {
            rb.velocity = transform.right * speed * -1; //launch the projectile left by multiplying by -1
            bulletSprite.flipX = true;                  //flip the spawn point since the player is facing left
        }
        //myTimer.Start();
        
    }

  



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")  //ground check for bullet
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Boss")   //boss check for bullet
        {
            status.DepleteHealth();
            float xScale = boss.localScale.x + 0.025f;
            float yScale = boss.localScale.y + 0.025f;
            float zScale = boss.localScale.z;
            boss.localScale = new Vector3(xScale, yScale, zScale);
            ScoreScript.scoreValue += 5;
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame

}

/// <summary>
/// helper class for the bullets, used for unit testing
/// </summary>
public class BulletHelper
{

    /// <summary>
    /// checks the direction of the player for the bullet logic
    /// </summary>
   public void checkDirection(ref float localScale, ref bool isFacingRight)
    {
                                            //check if player is facing right
        if (localScale == 1)
        {
            isFacingRight = true;
        }
        if (localScale == -1)              //check if the player is facing left
        {
            isFacingRight = false;
        }
    }

}
