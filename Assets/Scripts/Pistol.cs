using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : PistolBullet
{

    private int bulletsInMag = PISTOL_MAG_SIZE;
    private float timeShot = 0;

    public Transform firePoint;
    public GameObject pistolBullet;

    void Update()
    {
       
       if (timeShot <= 0)
       {
           if (bulletsInMag != 0 && Input.GetKeyDown(KeyCode.Q))
           {
               Shoot();
               bulletsInMag--;
               timeShot = PISTOL_SPEED_FIRE;
           }
       }
       else
       {
           timeShot -= Time.deltaTime;
       }
      
       if (Input.GetKeyDown(KeyCode.R))
       {
           bulletsInMag = PISTOL_MAG_SIZE;
       }
     
    }

    public void Shoot()
    {
        Instantiate(pistolBullet, firePoint.position, firePoint.rotation);
    }
}

