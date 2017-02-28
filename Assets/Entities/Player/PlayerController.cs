using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float speed = 15.0f;
	public float padding = 1.0f;

	float xmin;
	float xmax;

	void Start ()
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}

	void Update ()
	{
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

}
