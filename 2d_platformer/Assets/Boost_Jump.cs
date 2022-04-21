using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost_Jump : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] int force;
    [SerializeField] PlayerMovement pm;
    [SerializeField] AudioSource boostJump;
    // Start is called before the first frame update
    void Start()
    {
        pm.movementSpeed = 12f;
        pm.jumpForce = 1200f;
    }

    // Update is called once per frame
    void Update()
    {
        pm.movementSpeed = 12f;
        pm.jumpForce = 1200f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.AddForce(Vector2.up * force);
            boostJump.Play();
        }
    }
}
