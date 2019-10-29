using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidController2 : MonoBehaviour
{
    
	struct KidState
    {
    	public bool isAlive;
        public bool isGrounded;
        public bool isMoving;
        public bool isJuiced;
    }

	public float timeSwitchingTracks;
	public float jumpHeight = 400f;
   
    private TracksEnum currentTrack = TracksEnum.MIDDLE;
	private int btwnTrackDistance = 10;
	private float extraGravity = 9.8f;
    private KidState kidState;
	private Rigidbody rigidBody;
	private float t;
	private float startPosition;
	private float target;

    // Start is called before the first frame update
    void Start()
    {
        kidState.isMoving = false;
        startPosition = target = transform.localPosition.x;
        rigidBody = GetComponent<Rigidbody>();
        kidState.isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
    	//Jumping
    	if(kidState.isGrounded){
    		if(Input.GetAxis("Vertical") > 0){
    			rigidBody.AddForce(Vector3.up * jumpHeight);
    		}
    	}else{
		   Vector3 vel = rigidBody.velocity;
		   vel.y -= extraGravity * Time.deltaTime;
		   rigidBody.velocity=vel;
    	}


    	if(kidState.isMoving && (transform.localPosition.x != target)){ //Move to the target
    		t += Time.deltaTime/timeSwitchingTracks; 
        	transform.localPosition = new Vector3(Mathf.Lerp(startPosition, target, t), transform.localPosition.y, transform.localPosition.z);
    	}else{ //If not moving
    		kidState.isMoving = false;
    		//Receive input and determine if should change tracks
	    	float direction = Input.GetAxis("Horizontal");
	        if(direction != 0){
	        	changeTrack(direction);
	        }
    	}
    }

    //true = right, false = left
    private void changeTrack(float direction){
    	float destination = 0f;
    	bool willMove = false;
    	switch(currentTrack){
    		case TracksEnum.LEFT:{
    			if(direction > 0){ //Can only move right
    				willMove = true;
    				destination = transform.localPosition.x + btwnTrackDistance;
    				currentTrack = TracksEnum.MIDDLE;
    			}
    		}
    		break;
    		case TracksEnum.MIDDLE:{
    			willMove = true;
    			if(direction < 0){ //Move left
    				destination = transform.localPosition.x - btwnTrackDistance;
    				currentTrack = TracksEnum.LEFT;
    			}else{ //Move right
    				destination = transform.localPosition.x + btwnTrackDistance;
    				currentTrack = TracksEnum.RIGHT;
    			}
    		}
    		break;
    		case TracksEnum.RIGHT:{ //Can only move left
    			if(direction < 0){
    				willMove = true;
    				destination = transform.localPosition.x - btwnTrackDistance;
    				currentTrack = TracksEnum.MIDDLE;
    			}
    		}
    		break;
    	}

    	if(willMove){
    		setDestination(destination, timeSwitchingTracks);
    		kidState.isMoving = true;
    	}
    }

    private void setDestination(float destination, float time){
            t = 0;
            startPosition = transform.localPosition.x;
            timeSwitchingTracks = time;
            target = destination; 
    } 

     //For further collisions, wont use to jump for now
    void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Ground")
	    {
	        kidState.isGrounded = true;
	    }
	}
	 
	void OnCollisionExit(Collision other)
	{
	    if (other.gameObject.tag == "Ground")
	    {
	        kidState.isGrounded = false;
	    }
	}

	void OnTriggerEnter(Collider other)
	{
		// Debug.Log("Trigger with " +  other.name + " tagged " + other.tag );

		var obs = other.GetComponent<Obstacle>();
		if (obs) {
			Debug.Log("Other is Obstacle");
			ObstacleHit(obs);
		}
	}

	/// <summary>
	/// Handles logic/animation when hitting an obstacle
	/// </summary>
	void ObstacleHit(Obstacle other)
	{
	}
}
