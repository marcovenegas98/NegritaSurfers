using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KidController : MonoBehaviour
{
	enum Position { LEFT, CENTER, RIGHT};
	[SerializeField]
	private Position current;
	private Rigidbody rb;
	public float fowardSpeed = 10;
	public float horizontalSpeed = 30;
	private Vector3 desiredDirection;
	// Start is called before the first frame update 
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		current = Position.CENTER;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)  && current != Position.LEFT)
		{
			current--;
		}
		else if (Input.GetKeyDown(KeyCode.D) && current != Position.RIGHT)
		{
			current++;
		}

		if (current == Position.LEFT)
		{
			desiredDirection = new Vector3(-9, rb.position.y, rb.position.z);
		}
		else if (current == Position.CENTER)
		{
			desiredDirection = new Vector3(0, rb.position.y, rb.position.z);
		}
		else
		{
			desiredDirection = new Vector3(9, rb.position.y, rb.position.z);
		}

		rb.position = Vector3.MoveTowards(rb.position, desiredDirection, maxDistanceDelta: Time.deltaTime * horizontalSpeed);
		rb.position = Vector3.MoveTowards(rb.position, rb.position + Vector3.forward, maxDistanceDelta: Time.deltaTime * fowardSpeed);

	}
}
