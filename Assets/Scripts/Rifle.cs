using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : RifleBullet
{

    private int bulletsInMag = RIFLE_MAG_SIZE;
    private float timeShot = 0;
    private int currentRifleAmmo = RIFLE_AMMO;

    public Transform firePoint;
    public GameObject rifleBullet;

    void Update()
    {

        if (timeShot <= 0)
        {
            if (bulletsInMag != 0 && Input.GetKey(KeyCode.Q))
            {
                Shoot();
                bulletsInMag--;
                timeShot = RIFLE_SPEED_FIRE;
            }
        }
        else
        {
            timeShot -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentRifleAmmo != 0
            && bulletsInMag != RIFLE_MAG_SIZE)
        {
            while (bulletsInMag != RIFLE_MAG_SIZE && currentRifleAmmo != 0)
            {
                bulletsInMag++;
                currentRifleAmmo--;
            }

        }

    }

    public void Shoot()
    {
        Instantiate(rifleBullet, firePoint.position, firePoint.rotation);
    }


}
    

