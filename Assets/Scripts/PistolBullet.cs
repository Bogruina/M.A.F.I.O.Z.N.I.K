using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : MonoBehaviour
{
	public const int PISTOL_MAG_SIZE = 5;
	public const float PISTOL_SPEED_FIRE = 0.3f;
	public const int PISTOL_BULLET_DAMAGE = 2;
	public const float PISTOL_BULLET_SPEED = 2f;

	private float pistolBulletSpeed = PISTOL_BULLET_SPEED;
	private int pistolBulletDamage = PISTOL_BULLET_DAMAGE;
	public Rigidbody2D rb;

	private void Start()
	{
		rb.velocity = transform.right * GetPistolBulletSpeed();
	}

	public void SetPistolBulletDamage(int damage)
	{
		if (damage < 0)
		{
			throw new System.Exception();
		}
		this.pistolBulletDamage = damage;
	}

	public void SetPistolBulletSpeed(float speed)
	{
		if (speed < 0)
		{
			throw new System.Exception();
		}
		this.pistolBulletSpeed = speed;
	}

	public float GetPistolBulletSpeed()
	{
		return this.pistolBulletSpeed;
	}

	public int GetPistolBulletDamage()
	{
		return this.pistolBulletDamage;
	}

	private void OnTriggerEnter2D(Collider2D hitInfo)
	{
		Enemy enemy = hitInfo.GetComponent<Enemy>();

		if (enemy != null)
		{
			enemy.TakeDamage(GetPistolBulletDamage());
			Destroy(gameObject);
		}

		if (hitInfo.tag == "ground" || hitInfo.tag == "other")
		{
			Destroy(gameObject);
		}
	}
}
