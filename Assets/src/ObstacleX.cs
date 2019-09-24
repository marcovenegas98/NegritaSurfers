using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleX : Obstacle
{
    // Start is called before the first frame update
    protected void Start()
    {
        //Every X-Obstacle starts looking left or right.
        base.Start();
        determineOrientation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void determineOrientation(){
        if(Random.Range(0.0f, 1.0f) < 0.5f){
            transform.Rotate(0, 90.0f, 0);
        }else{
            transform.Rotate(0, -90.0f, 0);
        }
    }
}
