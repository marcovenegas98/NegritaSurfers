using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatController : MonoBehaviour
{
	public int track;
	public float timeSwitchingTracks;

	private bool moving;
	private float t;
	private Vector3 startPosition;
	private Vector3 target;
	private IEnumerator coroutine;

	private int btwnTrackDistance = 9;


    // Start is called before the first frame update
    void Start()
    {
    	moving = false;
        startPosition = target = transform.position;
        coroutine = changeTrack();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
    	if(moving && (transform.position != target)){ //Move to the target
    		t += Time.deltaTime/timeSwitchingTracks; 
        	transform.position = Vector3.Lerp(startPosition, target, t);
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
		    		case 0: {//Left track, move to middle track, looking left
		    			destination = new Vector3(transform.position.x + btwnTrackDistance, transform.position.y, transform.position.z);
		    			changeOrientationRight();
		    			track = 1;
		    		}break;
		    		case 1: { //Middle track, move to either
		    			if(Random.Range(0.0f, 1.0f) < 0.5f){
		    				//Move to left
		    				destination = new Vector3(transform.position.x - btwnTrackDistance, transform.position.y, transform.position.z);
		    				changeOrientationLeft();
		    				track = 0;
						}else{
							//Move to right
							destination = new Vector3(transform.position.x + btwnTrackDistance, transform.position.y, transform.position.z);
							changeOrientationRight();
							track = 2;
						}
		    		}break;
		    		case 2: {//Right track, move to middle track
		    			destination = new Vector3(transform.position.x - btwnTrackDistance, transform.position.y, transform.position.z);
		    			changeOrientationLeft();
		    			track = 1;
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
            startPosition = transform.position;
            timeSwitchingTracks = time;
            target = destination; 
    }
}