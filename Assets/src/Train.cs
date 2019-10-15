using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : Obstacle
{
	public bool moves; //Every train should be told if it moves or not
	private static Vector3 kidPosition = new Vector3(0f,0f,0f);
	private static float nearFactor = 50f;
	void Start()
	{
		base.Start();
	}

	void Update()
	{
		if(isNearPlayer()){
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
		Debug.Log("Se mueve");
	}

}
