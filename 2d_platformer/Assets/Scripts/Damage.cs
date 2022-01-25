using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject boss;
    public Rigidbody2D bossRB;
    public Transform player;
    public Bossman_run br;
    public PlayerMovement pm;
    public Rigidbody2D playerRB;
    
    
    public bool isHit = false;
    // Start is called before the first frame update
    void Start()
    {
        br = GetComponent<Bossman_run>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       /*if (collision.tag == "Player")
        {
            isHit = true;
            Health health = collision.GetComponent<Health>();
            health.TakeDamage(1f);
            //pm.SetCanMove(false);
            Vector2 bounce = new Vector2(20f, 0f);
            bossRB.AddForce(bounce);
            


        } */
    }
}
