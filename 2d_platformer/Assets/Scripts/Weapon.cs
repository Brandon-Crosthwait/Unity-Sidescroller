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
        if (myTimer.IsRunning)
        {
            if (myTimer.ElapsedMilliseconds >= 500)
            {
                myTimer.Stop();
                myTimer.Reset();
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (myTimer.IsRunning)
            {
                //do nothing
            }
            else
            {
                myTimer.Start();
                Shoot();
            }
           
        }

        void Shoot()
        {
            boomerangSound.Play();
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
