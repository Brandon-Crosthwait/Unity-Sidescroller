using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Boss_Entry : MonoBehaviour
{
    public Animator animator;
    public GameObject trigger;
    public Camera mainCam;
    public Camera bossCam;
    public GameObject firstTrigger;
    public Enter_Boss eb;
    public GameObject player;
    public PlayerMovement pm;
    public AudioSource fallSound;
    public Rigidbody2D Boss;

    // Start is called before the first frame update
    void Start()
    {
        eb = firstTrigger.GetComponent<Enter_Boss>();
        pm = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eb.time.ElapsedMilliseconds >= 3500)
        {
            mainCam.enabled = true;
            bossCam.enabled = false;
            eb.time.Stop();
            pm.SetCanMove(true);
            trigger.SetActive(false);
            firstTrigger.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.name == "Boss")
        {
            fallSound.Play();
            animator.SetTrigger("Boss_Has_Fallen");
            //Boss.isKinematic = true;
            //mainCam.enabled = true;
            //bossCam.enabled = false;
            
            
        }
    }
}
