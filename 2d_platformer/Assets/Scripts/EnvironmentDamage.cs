using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Health playerHealth;
    private int characterSelected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && playerHealth.currentHealth != 0)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            collision.GetComponent<PlayerMovement>().GetHit();
        }
    }
}
