using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float speed = 15.0f;
	public float padding = 1.0f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate;
	public float health = 500f;
	public AudioClip fireSound;

	float xmin;
	float xmax;

	void Start ()
	{
		UnitBoundaries();
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire", 0.0f, firingRate);
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("Fire");
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			//transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			//transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		// restrict the player to the gamespace
		float xClamp = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(xClamp, transform.position.y, transform.position.z);
	}

	void UnitBoundaries()
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}

	// Instantiates beam projectile as a GameObject (instead of a regular default object instantiation) and launches it at a certain velocity.
	void Fire()
	{
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0,projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("enemyLazer"))
		{
			EnemyProjectile enemyMissile = col.gameObject.GetComponent<EnemyProjectile>();
			health -= enemyMissile.EnemyDamage();
			enemyMissile.EnemyHit();
			if (health <= 0)
			{
				Destroy(gameObject);
			}
		}
	}

}
