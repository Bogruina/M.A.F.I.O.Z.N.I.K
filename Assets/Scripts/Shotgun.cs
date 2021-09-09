using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : ShotgunBullet
{
    
    private int bulletsInMag = SHOTGUN_MAG_SIZE;
    private int currentShotgunAmmo = SHOTGUN_AMMO;
    private float timeShot = 0;

    public Transform firePoint;
    public GameObject shotgunBullet;

    void Update()
    {

        if (timeShot <= 0)
        {
            if (bulletsInMag != 0 && Input.GetKeyDown(KeyCode.Q))
            {
                Shoot();
                bulletsInMag--;
                timeShot = SHOTGUN_SPEED_FIRE;
            }
        }
        else
        {
            timeShot -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentShotgunAmmo != 0 
            && bulletsInMag != SHOTGUN_MAG_SIZE)
        {
            while(bulletsInMag != SHOTGUN_MAG_SIZE && currentShotgunAmmo != 0)
{
                bulletsInMag++;
                currentShotgunAmmo--;
            }

        }

    }

    public void Shoot()
    {
        Instantiate(shotgunBullet, firePoint.position, firePoint.rotation);
    }
}
