using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float speed = 15.0f;
	public float padding = 1.0f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate;
	public float health = 500.0f;
	public AudioClip fireSound;

	private float nextFire = 0.0f;

	float xmin;
	float xmax;

	void Start ()
	{
		UnitBoundaries();
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFire)
		{
			InvokeRepeating("Fire", 0.0f, firingRate);
		}

		if (Input.GetKeyDown(KeyCode.Space) && Time.time < nextFire)
		{
			float fireDelay = nextFire - Time.time;
			InvokeRepeating("Fire", fireDelay, firingRate);
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
	// nextFire is referenced in Update() in order to stop the player from firing faster than intended by pressing the spacebar down repeatedly and rapidly.
		nextFire = Time.time + firingRate;
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
				LoadWinScreen();
				Die();
			}
		}
	}

	void Die()
	{
		Destroy(gameObject);
	}

	void LoadWinScreen()
	{
		LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		levelManager.LoadLevel("Win Screen");
	}

}
