using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goat : ObstacleX
{
	public float timeSwitchingTracks;

	private bool moving;
	private float t;
	private Vector3 startPosition;
	private Vector3 target;
	private IEnumerator coroutine;

	private int btwnTrackDistance = 10;


    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    	moving = false;
        startPosition = target = transform.localPosition;
        coroutine = changeTrack();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
    	if(moving && (transform.localPosition != target)){ //Move to the target
    		t += Time.deltaTime/timeSwitchingTracks; 
        	transform.localPosition = Vector3.Lerp(startPosition, target, t);
    	}else{
    		moving = false;
    	}
    }

    //Co-routine to change track every random seconds
    private IEnumerator changeTrack(){ 
    	while(true){
    		if(!moving){
    			Vector3 destination = new Vector3(0.0f, 0.0f, 0.0f);
		    	switch(track){
		    		case TracksEnum.LEFT: {//Left track, move to middle track, looking left
		    			destination = new Vector3(transform.localPosition.x + btwnTrackDistance, transform.localPosition.y, transform.localPosition.z);
		    			changeOrientationRight();
		    			track = TracksEnum.MIDDLE;
		    		}break;
		    		case TracksEnum.MIDDLE: { //Middle track, move to either
		    			if(Random.Range(0.0f, 1.0f) < 0.5f){
		    				//Move to left
		    				destination = new Vector3(transform.localPosition.x - btwnTrackDistance, transform.localPosition.y, transform.localPosition.z);
		    				changeOrientationLeft();
		    				track = TracksEnum.LEFT;
						}else{
							//Move to right
							destination = new Vector3(transform.localPosition.x + btwnTrackDistance, transform.localPosition.y, transform.localPosition.z);
							changeOrientationRight();
							track = TracksEnum.RIGHT;
						}
		    		}break;
		    		case TracksEnum.RIGHT: {//Right track, move to middle track
		    			destination = new Vector3(transform.localPosition.x - btwnTrackDistance, transform.localPosition.y, transform.localPosition.z);
		    			changeOrientationLeft();
		    			track = TracksEnum.MIDDLE;
		    		}break;
		    	}
		    	setDestination(destination, timeSwitchingTracks);
		    	moving = true;
    		}
    		float timeToMoveAgain = Random.Range(2.0f, 4.0f);
    		yield return new WaitForSeconds(timeToMoveAgain);
    	}
    }

    private void changeOrientationRight(){
    	transform.rotation = Quaternion.identity;
    	transform.Rotate(0, 90.0f, 0);
    }

    private void changeOrientationLeft(){
    	transform.rotation = Quaternion.identity;
    	transform.Rotate(0, -90.0f, 0);
    }

    private void setDestination(Vector3 destination, float time){
            t = 0;
            startPosition = transform.localPosition;
            timeSwitchingTracks = time;
            target = destination; 
    }
}