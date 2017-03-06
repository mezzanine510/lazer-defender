using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

	public float damage = 100f;

	public float EnemyDamage()
	{
		return damage;
	}

	public void EnemyHit()
	{
		Destroy(gameObject);
	}
}
