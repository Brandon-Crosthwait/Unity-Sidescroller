using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossman_run : MonoBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 3f;

    public Transform player;
    public Rigidbody2D rb;
    Boss boss;
    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        boss = gameObject.GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
           //Health health = player.GetComponent<Health>();
            //health.TakeDamage(0.00001f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Health health = other.GetComponent<Health>();
            health.TakeDamage(1f);
        }
    }
}
