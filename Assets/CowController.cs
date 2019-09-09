using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    public int track; //Use to place on map according to its track.
    // Start is called before the first frame update
    void Start()
    {
    	//Determine orientation
        if(Random.Range(0.0f, 1.0f) < 0.5f){
        	transform.Rotate(0, 90.0f, 0);
        }else{
        	transform.Rotate(0, -90.0f, 0);
        }

        //Determine track
        float laneRandom = Random.Range(0.0f, 1.0f);
        if(laneRandom < 0.33){
        	track = 0;
        }else if(laneRandom < 0.66){
        	track = 1;
    	}else{
    		track = 2;
    	}


    }

    // Update is called once per frame
    void Update()
    {

    }
}
