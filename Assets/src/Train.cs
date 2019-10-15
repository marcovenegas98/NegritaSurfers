using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : Obstacle
{
	public bool moves; //Every train should be told if it moves or not
	private bool moved; //Flag to tell if it already moved.
	private bool moving; //Flag to tell if it is moving.
	private float t;
	private Vector3 startPosition;
	private Vector3 target;
	private IEnumerator coroutine;
	private static Vector3 kidPosition = new Vector3(0f,0f,0f);
	private static float nearFactor = 50f;
	private static float distanceToMove = 50f;
	private static float secondsMoving = 1f;
	
	void Start()
	{
		base.Start();
		setDestination(secondsMoving);
		moved = false;
        //coroutine = changeTrack();
        //StartCoroutine(coroutine);
	}

	void Update()
	{
		if(isNearPlayer()){
			move();
		}
	}

	private bool isNearPlayer(){
		bool result = false;
		if(transform.localPosition.z < nearFactor){
			result = true;
		}
		return result;
	}

	private void move(){
		if(!moved && !moving){
			if(isNearPlayer()){
				if(moving && (transform.localPosition != target)){ //Move to the target
		    		t += Time.deltaTime/timeSwitchingTracks; 
		        	transform.localPosition = Vector3.Lerp(startPosition, target, t);
		    	}else{
		    		moving = false;
		    	}
			}
		}
	}

	private void setDestination(float time){
    	Vector3 destination = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + distanceToMove);
    	t = 0;
        startPosition = transform.localPosition;
        target = destination; 
    	//moving = true;
    }



}
