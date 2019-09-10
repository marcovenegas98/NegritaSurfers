using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatController : MonoBehaviour
{
	public int track;

	bool moving;
	public float timeSwitchingTracks;
	float t;
	Vector3 startPosition;
	Vector3 target;
	IEnumerator coroutine;


    // Start is called before the first frame update
    void Start()
    {
    	moving = false;
    	determineOrientation();
        determineTrackStart();
        startPosition = target = transform.position;
        coroutine = changeTrack();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
    	if(moving && (transform.position != target)){
    		t += Time.deltaTime/timeSwitchingTracks;
        	transform.position = Vector3.Lerp(startPosition, target, t);
    	}else{
    		moving = false;
    	}
    }

    private void determineOrientation(){
        if(Random.Range(0.0f, 1.0f) < 0.5f){
            transform.Rotate(0, 90.0f, 0);
        }else{
            transform.Rotate(0, -90.0f, 0);
        }
    }

    private void determineTrackStart(){
    	float trackRandom = Random.Range(0.0f, 1.0f);
        if(trackRandom < 0.33){
            track = 0;
            transform.position = new Vector3(-9.0f, transform.position.y, transform.position.z);
        }else if(trackRandom < 0.66){
            track = 1;
            transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);
        }else{
            track = 2;
            transform.position = new Vector3(9.0f, transform.position.y, transform.position.z);
        }
    }

    //Co-routine to change track every random seconds
    private IEnumerator changeTrack(){ 
    	while(true){
    		if(!moving){
    			Vector3 destination = new Vector3(0.0f, 0.0f, 0.0f);
		    	switch(track){
		    		case 0: {//Left track, move to middle track, looking left, rotate 180
		    			destination = new Vector3(0.0f, transform.position.y, transform.position.z);
		    			changeOrientationRight();
		    			track = 1;
		    			transform.Rotate(0, 180.0f, 0);
		    		}break;
		    		case 1: { //Middle track, move to either
		    			if(Random.Range(0.0f, 1.0f) < 0.5f){
		    				//Move to left
		    				destination = new Vector3(-9.0f, transform.position.y, transform.position.z);
		    				changeOrientationLeft();
		    				track = 1;
						}else{
							//Move to right
							destination = new Vector3(9.0f, transform.position.y, transform.position.z);
							changeOrientationRight();
							track = 2;
						}
		    		}break;
		    		case 2: {//Right track, move to middle track
		    			destination = new Vector3(0.0f, transform.position.y, transform.position.z);
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
