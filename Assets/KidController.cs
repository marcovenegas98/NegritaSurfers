using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KidController : MonoBehaviour
{
	enum Position { LEFT = -1, CENTER = 0, RIGHT = 1};
	[SerializeField]
	public float fowardSpeed = 10;
	public float horizontalSpeed = 30;
	public float distanceLanes = 9;
	private Position current;
	private Rigidbody rb;
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

		desiredDirection = new Vector3(distanceLanes * (int)current, rb.position.y, rb.position.z);

		rb.position = Vector3.MoveTowards(rb.position, desiredDirection, maxDistanceDelta: Time.deltaTime * horizontalSpeed);
		//rb.position = Vector3.MoveTowards(rb.position, rb.position + Vector3.forward, maxDistanceDelta: Time.deltaTime * fowardSpeed);

	}
}
