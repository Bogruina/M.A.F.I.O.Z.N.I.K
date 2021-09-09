using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : MonoBehaviour
{
    public const int RIFLE_MAG_SIZE = 10;
	public const float RIFLE_SPEED_FIRE = 0.2f;
	public const int RIFLE_BULLET_DAMAGE = 1;
	public const float RIFLE_BULLET_SPEED = 2f;
	public const int RIFLE_AMMO = 30;

	private float rifleBulletSpeed = RIFLE_BULLET_SPEED;
	private int rifleBulletDamage = RIFLE_BULLET_DAMAGE;
	public Rigidbody2D rb;

	private void Start()
	{
		rb.velocity = transform.right * GetRifleBulletSpeed();
	}
	
	public void SetRifleBulletDamage(int damage)
	{
		if (damage < 0)
		{
			throw new System.Exception();
		}
		this.rifleBulletDamage = damage;
	}

	public void SetRifleBulletSpeed(float speed)
	{
		if (speed < 0)
		{
			throw new System.Exception();
		}
		this.rifleBulletSpeed = speed;
	}

	public float GetRifleBulletSpeed()
	{
		return this.rifleBulletSpeed;
	}

	public int GetRifleBulletDamage()
	{
		return this.rifleBulletDamage;
	}

	private void OnTriggerEnter2D(Collider2D hitInfo)
	{
		Enemy enemy = hitInfo.GetComponent<Enemy>();

		if (enemy != null)
		{
			enemy.TakeDamage(GetRifleBulletDamage());
			Destroy(gameObject);
		}

		if (hitInfo.tag == "ground" || hitInfo.tag == "other")
		{
			Destroy(gameObject);
		}
	}
}
