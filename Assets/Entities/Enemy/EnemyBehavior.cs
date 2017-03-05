using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public float health = 200f;

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
		else
		{
			Debug.Log("You got hit by a " + col.gameObject.name + "!");
		}

	}
}
