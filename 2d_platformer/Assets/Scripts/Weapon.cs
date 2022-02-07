using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Stopwatch myTimer = new Stopwatch();
    public AudioSource boomerangSound;

    // Start is called before the first frame update
    void Start()
    {
         
      
    }

    // Update is called once per frame
    void Update()
    {
        if (myTimer.IsRunning)  //check if the bullet timer is running every frame
        {
            if (myTimer.ElapsedMilliseconds >= 500)  //if it has been half a second since last bullet/projectile is thrown, reset timer
            {
                myTimer.Stop();
                myTimer.Reset();
            }
        }

        if (Input.GetButtonDown("Fire1"))    //get right mouse button
        {
            if (myTimer.IsRunning)
            {
                //do nothing
            }
            else                          //check to see if there is a bullet cooldown timerr
            {
                myTimer.Start();        //start the cooldown timer 
                Shoot();               //shoot method to instantiate new bullet
            }
           
        }

        void Shoot()
        {
            boomerangSound.Play();  //play projectile sound
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);   //instantiate the new bullet, logic for flight is in bullet script
        }
    }
}
