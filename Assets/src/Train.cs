using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : Obstacle
{
	public bool moves; //Every train should be told if it moves or not
	private static float nearFactor = 100f;
	private static float speed = 2f;
	
	void Start()
	{
		base.Start();
	}

	void Update()
	{
		base.Update();
		if(isNearPlayer() && moves){
			move();
		}
	}

	private bool isNearPlayer(){
		bool result = false;
		if(transform.position.z < nearFactor){
			result = true;
		}
		return result;
	}

	private void move(){
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - speed);
	}



}
