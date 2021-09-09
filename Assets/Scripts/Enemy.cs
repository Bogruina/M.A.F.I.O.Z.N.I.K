using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 5;
	public float speed;
	bool moveingRight;
	public int positionOfPatrol;
	public Transform point;
	Transform player;
	public float stoppingDistace;
	bool chill = false;
	bool angry = false;
	bool goBack = false;
	public float rayDistance = 3f;
	private Rigidbody2D rb;
	public Animator anim;

	void Start()
	{
		health = health * 2;
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		Physics2D.queriesStartInColliders = false;
	}

	void Update()
	{
		if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
		{
			chill = true;
		}

		if (Vector2.Distance(transform.position, player.position) < stoppingDistace)
		{
			angry = true;
			chill = false;
			goBack = false;
		}

		if (Vector2.Distance(transform.position, player.position) > stoppingDistace)
		{
			goBack = true;
			angry = false;
		}

		if (chill == true)
		{
			Chill();
		}
		else if (angry == true)
		{
			Angry();
		}
		else if (goBack == true)
		{
			GoBack();
		}
	}

	void Chill()
	{
		if (transform.position.x > point.position.x + positionOfPatrol)
		{
			moveingRight = false;
			transform.eulerAngles = new Vector3(0, -180, 0);
		}
		else if (transform.position.x < point.position.x - positionOfPatrol)
		{
			moveingRight = true;
			transform.eulerAngles = new Vector3(0, 0, 0);
		}

		if (moveingRight)
		{
			transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, rayDistance);
			if (hit.collider != null)
			{
				anim.SetBool("isJump", true);
				rb.velocity = Vector2.up * 3.5f;
			}
			else
			{
				anim.SetBool("isJump", false);
			}
		}
		else
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, rayDistance);
			if (hit.collider != null)
			{
				anim.SetBool("isJump", true);
				rb.velocity = Vector2.up * 3.5f;
			}
			else
			{
				anim.SetBool("isJump", false);
			}
			transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
		}
	}

    public void TakeDamage(int damage)
	{
		health -= damage;

		if(health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);
	}

	void Angry()
	{
		transform.eulerAngles = new Vector3(0, -180, 0);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, rayDistance);
		if (hit.collider != null)
		{
			anim.SetBool("isJump", true);
			rb.velocity = Vector2.up * 3.5f;
		}
		else
		{
			anim.SetBool("isJump", false);
		}
		transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
	}

	void GoBack()
	{
		transform.eulerAngles = new Vector3(0, 0, 0);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, rayDistance);
		if (hit.collider != null)
		{
			anim.SetBool("isJump", true);
			rb.velocity = Vector2.up * 3.5f;
		}
		else
		{
			anim.SetBool("isJump", false);
		}
		transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * rayDistance);

		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * rayDistance);
	}
}
