using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidController : MonoBehaviour
{	
	public float speed = 10;
	public Vector3 left = new Vector3(-9, (float)3.4393, 0);
	public Vector3 center = new Vector3(0, (float)3.4393, 0);
	public Vector3 right = new Vector3(9, (float)3.4393, 0);
	public Vector3 currentPosition;
	public Vector3 desiredPosition;
	public float distance = 0;
	public bool isJumping = false; 
	private Rigidbody rb;
	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody>();
		rb.position = center;
		currentPosition = center;
		desiredPosition = center;
	}

	void Update()
	{
		distance = Vector3.Distance(rb.position, desiredPosition);
		
		if (distance < 0.15)
		{
			currentPosition = desiredPosition;
			if (Input.GetAxis("Horizontal") < 0)
			{
				if (currentPosition.Equals(center))
				{
					desiredPosition = left;
				}
				else if (currentPosition.Equals(right))
				{
					desiredPosition = center;
				}

			}
			else if (Input.GetAxis("Horizontal") > 0)
			{
				if (currentPosition.Equals(center))
				{
					desiredPosition = right;
				}
				else if (currentPosition.Equals(left))
				{
					desiredPosition = center;
				}

			}
		}
		else
		{
			rb.position = Vector3.MoveTowards(transform.position, desiredPosition, maxDistanceDelta: Time.fixedDeltaTime * speed);
		}
		
	}
// Update is called once per frame
void FixedUpdate()
    {
		
	}
}
