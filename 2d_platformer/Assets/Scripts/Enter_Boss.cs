using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enter_Boss : MonoBehaviour
{
    public Camera mainCam;
    public Camera bossCam;
    public GameObject boss;
    public GameObject trigger;
    public GameObject player;
    public Stopwatch time = new Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boss.SetActive(true);
            //GameObject cam = bossCam.GetComponent<GameObject>();
            //cam.SetActive(true);
            mainCam.enabled = false;
            bossCam.enabled = true;
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            pm.SetCanMove(false);
            //trigger.SetActive(false);
            time.Start();
        }
        
        
    }
    
}
