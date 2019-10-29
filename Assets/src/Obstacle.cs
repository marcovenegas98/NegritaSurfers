using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
	protected TracksEnum track;
    // Start is called before the first frame update
    protected void Start()
    {
        determineTrack();
    }

    protected void Update(){
        if(transform.position.z < -100f){
            Destroy(gameObject);
        }
    }

    void determineTrack(){
    	//At the start, every obstacle can only have 3 posible values for x, -10, 0 and 10.
    	switch(transform.position.x){
    		case -10f: track = TracksEnum.LEFT;
    		break;
    		case 0f: track = TracksEnum.MIDDLE;
    		break;
    		case 10f: track = TracksEnum.RIGHT;
    		break;
    	}
    }
}
