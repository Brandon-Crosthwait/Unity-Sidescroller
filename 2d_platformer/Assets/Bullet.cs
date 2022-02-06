using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public Transform player;
    public SpriteRenderer bulletSprite;
    public Stopwatch myTimer = new Stopwatch();
    public Boss_Status status;
    

    // Start is called before the first frame update
    void Start()
    {

        if (player.localScale.x == 1)
        {
            rb.velocity = transform.right * speed;
            bulletSprite.flipX = false;
        }
        if (player.localScale.x == -1)
        {
            rb.velocity = transform.right * speed * -1;
            bulletSprite.flipX = true;
        }
        //myTimer.Start();
    }

    private void Update()
    {
        if (myTimer.IsRunning)
        {
            if (myTimer.ElapsedMilliseconds >= 3000)
            {
                myTimer.Stop();
                myTimer.Reset();
                Destroy(gameObject);
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Boss")
        {
            status.DepleteHealth();
        }
        
    }

    // Update is called once per frame

}
