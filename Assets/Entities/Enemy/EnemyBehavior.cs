using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public float health = 200f;
	public GameObject enemyProjectile;
	public float enemyProjectileSpeed = 10f;
	public float enemyFiringRate = 0.5f;

	void Update()
	{
		float probability = Time.deltaTime * enemyFiringRate;
		if (Random.value < probability)
		{
			EnemyFire();
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "lazer")
		{
			Projectile missile = col.gameObject.GetComponent<Projectile>();
			health -= missile.GetDamage();
			missile.Hit();

			if (health <= 0)
			{
				Destroy(gameObject);
			}
		}

	}

	void EnemyFire()
	{
		Vector3 startPosition = transform.position + new Vector3(0, -0.5f, 0);
		GameObject enemyLazer = Instantiate(enemyProjectile, startPosition, Quaternion.identity) as GameObject;
		enemyLazer.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -enemyProjectileSpeed);
	}



}
