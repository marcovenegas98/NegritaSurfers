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
        public bool isGoingUp;
    }

    public GameObject ground;
    public GameObject roof;

	public float timeSwitchingTracks;
   
   	private Vector3 INITIAL_LOCATION = new Vector3(0, 3.338f, 0);

    private TracksEnum currentTrack = TracksEnum.MIDDLE;
	private int btwnTrackDistance = 10;
	private float extraGravity = 9.8f;
    private KidState kidState;
	private Rigidbody rigidBody;
	private float t;
	private float startPosition;
	private float target;
    private float terrainSpeedAugmentationFactor = 0.2f;
	public float jumpMultiplier;
	public float descentMultiplier;


    private void _initKid(){
        //Instantiate Ground below and Roof above.
        Vector3 actualRotation = new Vector3(0.0f, 0.0f, 0.0f);
        Instantiate(ground, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.Euler(actualRotation));
        Instantiate(roof, new Vector3(0.0f, 12.88516f, 0.0f), Quaternion.Euler(actualRotation));
        
        //Set Kid States
        kidState.isAlive = true;
        kidState.isGrounded = true;
        kidState.isMoving = false;
        kidState.isJuiced = false;
        kidState.isGoingUp = false;
        transform.position = INITIAL_LOCATION;
    }

    // Start is called before the first frame update
    void Start()
    {
        _initKid();
        startPosition = target = transform.position.x;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    	//Jumping
        if(kidState.isGoingUp){
            goUp();
        }else if (kidState.isGrounded && Input.GetAxis("Vertical") > 0){
            kidState.isGoingUp = true;
            goUp();
        }else if(!kidState.isGrounded){
            goDown();
        }

    	if(kidState.isMoving && (transform.position.x != target)){ //Move to the target
    		t += Time.deltaTime/timeSwitchingTracks; 
        	transform.position = new Vector3(Mathf.Lerp(startPosition, target, t), transform.position.y, transform.position.z);
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
    				destination = transform.position.x + btwnTrackDistance;
    				currentTrack = TracksEnum.MIDDLE;
    			}
    		}
    		break;
    		case TracksEnum.MIDDLE:{
    			willMove = true;
    			if(direction < 0){ //Move left
    				destination = transform.position.x - btwnTrackDistance;
    				currentTrack = TracksEnum.LEFT;
    			}else{ //Move right
    				destination = transform.position.x + btwnTrackDistance;
    				currentTrack = TracksEnum.RIGHT;
    			}
    		}
    		break;
    		case TracksEnum.RIGHT:{ //Can only move left
    			if(direction < 0){
    				willMove = true;
    				destination = transform.position.x - btwnTrackDistance;
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
            startPosition = transform.position.x;
            timeSwitchingTracks = time;
            target = destination; 
    } 

    void OnCollisionEnter(Collision other)
	{
		switch(other.gameObject.tag){
			case "Ground" : {
                if(!kidState.isGrounded){
                    Terrain.speed -= 0.5f;
                }
	        	kidState.isGrounded = true;
			}break;
			case "Roof" : {
				kidState.isGoingUp = false;
                goDown();
			}break;
		}
	}
	 
	void OnCollisionExit(Collision other)
	{
		switch(other.gameObject.tag){
			case "Ground" : {
	        	kidState.isGrounded = false;
                Terrain.speed += 0.5f;
			}break;
		}
	}

	void goUp(){
		Vector3 pos = transform.position;
		pos.y += jumpMultiplier;
		transform.position = pos;
	}

	void goDown(){
		Vector3 pos = transform.position;
		pos.y -= descentMultiplier;
		transform.position = pos;
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
