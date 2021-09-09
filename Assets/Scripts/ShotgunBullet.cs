using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
	public const int SHOTGUN_MAG_SIZE = 2;
	public const float SHOTGUN_SPEED_FIRE = 0.7f;
	public const int SHOTGUN_BULLET_DAMAGE = 8;
	public const float SHOTGUN_BULLET_SPEED = 1f;
	public const int SHOTGUN_AMMO = 4;

	private float rifleBulletSpeed = SHOTGUN_BULLET_SPEED;
	private int rifleBulletDamage = SHOTGUN_BULLET_DAMAGE;
	public Rigidbody2D rb;

	private void Start()
	{
		rb.velocity = transform.right * GetShotgunBulletSpeed();
	}

	public void SetShotgunBulletDamage(int damage)
	{
		if (damage < 0)
		{
			throw new System.Exception();
		}
		this.rifleBulletDamage = damage;
	}

	public void SetShotgunBulletSpeed(float speed)
	{
		if (speed < 0)
		{
			throw new System.Exception();
		}
		this.rifleBulletSpeed = speed;
	}

	public float GetShotgunBulletSpeed()
	{
		return this.rifleBulletSpeed;
	}

	public int GetShotgunBulletDamage()
	{
		return this.rifleBulletDamage;
	}

	private void OnTriggerEnter2D(Collider2D hitInfo)
	{
		Enemy enemy = hitInfo.GetComponent<Enemy>();

		if (enemy != null)
		{
			enemy.TakeDamage(GetShotgunBulletDamage());
			Destroy(gameObject);
		}

		if (hitInfo.tag == "ground" || hitInfo.tag == "other")
		{
			Destroy(gameObject);
		}
	}
}
