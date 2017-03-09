using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShredder : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("enemyLazer"))
		{
			Destroy(col.gameObject);
		}
	}

}
