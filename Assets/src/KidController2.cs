using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidController2 : MonoBehaviour
{
    
	public float timeSwitchingTracks;
	public float jumpHeight = 70f;
    private TracksEnum currentTrack = TracksEnum.MIDDLE;
	private bool moving;
	private float t;
	private float startPosition;
	private float target;
	private int btwnTrackDistance = 10;
	private Rigidbody rigidBody;
	private bool isGrounded;
	private float yLimit;
	private float extraGravity = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        startPosition = target = transform.localPosition.x;
        rigidBody = GetComponent<Rigidbody>();
        isGrounded = true;
        yLimit = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
    	//Jumping
    	if(isGrounded){
    		if(Input.GetAxis("Vertical") > 0){
    			rigidBody.AddForce(Vector3.up * jumpHeight);
    		}
    	}else{
		   Vector3 vel = rigidBody.velocity;
		   vel.y -= extraGravity * Time.deltaTime;
		   rigidBody.velocity=vel;
    	}


    	if(moving && (transform.localPosition.x != target)){ //Move to the target
    		t += Time.deltaTime/timeSwitchingTracks; 
        	transform.localPosition = new Vector3(Mathf.Lerp(startPosition, target, t), transform.localPosition.y, transform.localPosition.z);
    	}else{ //If not moving
    		moving = false;
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
    		moving = true;
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
	        isGrounded = true;
	    }
	}
	 
	void OnCollisionExit(Collision other)
	{
	    if (other.gameObject.tag == "Ground")
	    {
	        isGrounded = false;
	    }
	}
}
