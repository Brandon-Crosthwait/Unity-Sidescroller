using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    public GameObject projectile;
    public Transform shotArea;

    [SerializeField] private float fireRate;
    private float nextFire;

    private void ShootBullet()
    {
        Instantiate(projectile, shotArea.position, transform.rotation);
    }

    private void Start()
    {
        nextFire = Time.time;
    }

    private void Update()
    {
        CheckIfTimeToFire();
    }

    private void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            ShootBullet();
            nextFire = Time.time + fireRate;
        }
    }
}
