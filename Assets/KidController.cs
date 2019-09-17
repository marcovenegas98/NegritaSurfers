using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KidController : MonoBehaviour
{
	enum Position { LEFT = -1, CENTER = 0, RIGHT = 1};
	[SerializeField]
	public float fowardSpeed = 10f;
	public float horizontalSpeed = 30f;
	public float distanceLanes = 9f;
	public float fallMultiplier = 2.5f;
	public float jumpForce = 20f;
	private Position current;
	private Vector3 desiredDirection;
	public Animation anim;
	private Rigidbody rb;
	public GameObject ob;

	// Start is called before the first frame update 
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		current = Position.CENTER;
		anim = ob.GetComponent<Animation>();
	}

	void Update()
	{
		if (rb.velocity.y < 0) {
			rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
		if (Input.GetKeyDown(KeyCode.A)  && current != Position.LEFT)
		{
			current--;
		}
		else if (Input.GetKeyDown(KeyCode.D) && current != Position.RIGHT)
		{
			current++;
		}

		if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
		{
			rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
			anim.CrossFade("Jump");
		}
		
		desiredDirection = new Vector3(distanceLanes * (int)current, rb.position.y, rb.position.z);

		rb.position = Vector3.MoveTowards(rb.position, desiredDirection, maxDistanceDelta: Time.deltaTime * horizontalSpeed);
		//rb.position = Vector3.MoveTowards(rb.position, rb.position + Vector3.forward, maxDistanceDelta: Time.deltaTime * fowardSpeed);
	}

	private bool IsGrounded()
	{
		return rb.velocity.y == 0;
	}
}
