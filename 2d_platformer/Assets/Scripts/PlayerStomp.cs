using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStomp : MonoBehaviour
{
    public int damageDealt;


    private Rigidbody2D theRB2D;
    public float enemyBounceForce;
    public float objectBounceForce;

    // Start is called before the first frame update
    void Start()
    {
        theRB2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "StompArea")
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageDealt);
            theRB2D.AddForce(transform.up * enemyBounceForce, ForceMode2D.Impulse);
        }
        if(other.gameObject.tag == "BounceArea")
        {
            theRB2D.AddForce(transform.up * objectBounceForce, ForceMode2D.Impulse);
        }
    }

}
