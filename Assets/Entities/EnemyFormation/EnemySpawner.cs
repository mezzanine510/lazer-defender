using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;

	private bool movingRight = true;
	private float xmin;
	private float xmax;

	// Use this for initialization
	void Start ()
	{
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		xmax = rightBoundary.x;
		xmin = leftBoundary.x;

		SpawnUntilFull();

	}

	// Update is called once per frame
	void Update ()
	{
		if (movingRight)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

	// Keep formation from going outside the playspace
		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);

		if (leftEdgeOfFormation < xmin)
		{
			movingRight = true;
		}
		else if (rightEdgeOfFormation > xmax)
		{
			movingRight = false;
		}

		if (AllMembersDead())
		{
			SpawnUntilFull();
		}

	}

	// If an enemy exists in the child object position
	void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition();

		if (freePosition)
		{
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition())
		{
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}

	// If a child object (enemy) exists in a given position, return it. Otherwise, return null.
	Transform NextFreePosition()
	{
		foreach (Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount == 0)
			{
				return childPositionGameObject;
			}
		}
		return null;
	}

	// If there are more than 0 (1 or more) child objects (enemies) attached to the Enemy Spawner, return false. If all enemies are dead, return true.
	bool AllMembersDead()
	{
		foreach (Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount > 0)
			{
				return false;
			}
		}

		return true;
	}

	// Draw gizmo showing enemy formation in editor
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
	}
}
