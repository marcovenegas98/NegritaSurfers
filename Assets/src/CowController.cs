using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    public int track; //Use to place on map according to its track.
    // Start is called before the first frame update
    void Start()
    {
    	//determineOrientation();
        //determineTrack();
    }
    //Cow controller should not be responsible for this logic, will refactor
    /*
    void determineOrientation(){
        if(Random.Range(0.0f, 1.0f) < 0.5f){
            transform.Rotate(0, 90.0f, 0);
        }else{
            transform.Rotate(0, -90.0f, 0);
        }
    }

    void determineTrack(){
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
    */

    // Update is called once per frame
    void Update()
    {

    }
}
