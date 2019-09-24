using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateField : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cowShortPF;
    public GameObject cowTallPF;

    public int amountShort;
    public int amountTall;

    void Start()
    {
    	float x, xleft, xright, z, yRotation;
    	float yShort = 3.159f;
    	float yTall = 7.187f;
    	for(int i = 0; i < amountShort; ++i){
    		xleft = Random.Range(-50.0f, -20.0f);
    		xright = Random.Range(20.0f, 50.0f);

    		x = chooseRorL(xleft, xright);

    		z = Random.Range(25.0f, 250.0f);
    		yRotation = Random.Range(0.0f, 360.0f);
    		Vector3 actualRotation = new Vector3(0.0f, yRotation, 0.0f);
    		Instantiate(cowShortPF, new Vector3(x, yShort, z), Quaternion.Euler(actualRotation));
    	}

    	for(int i = 0; i < amountTall; ++i){
    		xleft = Random.Range(-50.0f, -20.0f);
    		xright = Random.Range(20.0f, 50.0f);

    		x = chooseRorL(xleft, xright);
    		z = Random.Range(25.0f, 250.0f);
    		yRotation = Random.Range(0.0f, 360.0f);
    		Vector3 actualRotation = new Vector3(0.0f, yRotation, 0.0f);
    		Instantiate(cowTallPF, new Vector3(x, yTall, z), Quaternion.Euler(actualRotation));
    	}
    }

    float chooseRorL(float left, float right){
    	if(Random.Range(0.0f, 1.0f) < 0.5){
    		return left;
    	}else{
    		return right;
    	}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
