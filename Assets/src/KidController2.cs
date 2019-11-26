using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidController2 : MonoBehaviour
{
    
	struct KidState
    {
    	public bool isAlive;
        public bool isGrounded;
        public bool isMoving;
        public bool isJuiced;
        public bool isGoingUp;
    }

    public GameObject ground;
    public GameObject roof;
	public GameObject gameOverPanel;

	public float timeSwitchingTracks;
   
   	private Vector3 INITIAL_LOCATION = new Vector3(0, 3.338f, 0);
    private TracksEnum currentTrack = TracksEnum.MIDDLE;
	private int btwnTrackDistance = 10;
    private KidState kidState;
	private Rigidbody rigidBody;
	private float t;
	private float startPosition;
	private float target;

    public float jumpHeight;
    public float jumpDist;
    public float jumpCooldownDist;


    private void _initKid(){
        //Instantiate Ground below and Roof above.
        Vector3 actualRotation = new Vector3(0.0f, 0.0f, 0.0f);
        //Instantiate(ground, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.Euler(actualRotation));
        //Instantiate(roof, new Vector3(0.0f, 12.88516f, 0.0f), Quaternion.Euler(actualRotation));
        
        //Set Kid States
        kidState.isAlive = true;
        kidState.isGrounded = true;
        kidState.isMoving = false;
        kidState.isJuiced = false;
        kidState.isGoingUp = false;
        transform.position = INITIAL_LOCATION;
    }

    // Start is called before the first frame update
    void Start()
    {
        _initKid();
        startPosition = target = transform.position.x;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (kidState.isAlive)
        {

            if (kidState.isGrounded && Input.GetAxis("Vertical") > 0){
                kidState.isGrounded = false;
                StartCoroutine("JumpCoroutine");
            }

    	    if(kidState.isMoving && (transform.position.x != target)){ //Move to the target
    		    t += Time.deltaTime/timeSwitchingTracks; 
        	    transform.position = new Vector3(Mathf.Lerp(startPosition, target, t), transform.position.y, transform.position.z);
    	    }else{ //If not moving
    		    kidState.isMoving = false;
    		    //Receive input and determine if should change tracks
	    	    float direction = Input.GetAxis("Horizontal");
	            if(direction != 0){
	        	    changeTrack(direction);
	            }
    	    }
        }
    }

    //true = right, false = left
    private void changeTrack(float direction){
    	float destination = 0f;
    	bool willMove = false;
    	switch(currentTrack){
    		case TracksEnum.LEFT:{
    			if(direction > 0){ //Can only move right
    				willMove = true;
    				destination = transform.position.x + btwnTrackDistance;
    				currentTrack = TracksEnum.MIDDLE;
    			}
    		}
    		break;
    		case TracksEnum.MIDDLE:{
    			willMove = true;
    			if(direction < 0){ //Move left
    				destination = transform.position.x - btwnTrackDistance;
    				currentTrack = TracksEnum.LEFT;
    			}else{ //Move right
    				destination = transform.position.x + btwnTrackDistance;
    				currentTrack = TracksEnum.RIGHT;
    			}
    		}
    		break;
    		case TracksEnum.RIGHT:{ //Can only move left
    			if(direction < 0){
    				willMove = true;
    				destination = transform.position.x - btwnTrackDistance;
    				currentTrack = TracksEnum.MIDDLE;
    			}
    		}
    		break;
    	}

    	if(willMove){
    		setDestination(destination, timeSwitchingTracks);
    		kidState.isMoving = true;
    	}
    }

    private void setDestination(float destination, float time){
            t = 0;
            startPosition = transform.position.x;
            timeSwitchingTracks = time;
            target = destination; 
    }

    /// <summary>
    /// Handles jumping behaviour.
    /// </summary>
    /// <remarks>
    /// The Y position is calculated as a parabola, a function of the distance traveled
    /// In this way, the jump remains consistent with increasing terrain speed.
    /// The formula is y = - 4*h/d^2 * x * ( x - d )
    /// where h is the jump height, d is the jump distance
    /// and x is the horizontal distance traveled since the start of the jump
    /// </remarks>
    /// <returns></returns>
    private IEnumerator JumpCoroutine()
    {
        float distanceTraveled, accel, yOffset;
        accel = - 4 * this.jumpHeight /  Mathf.Pow(jumpDist, 2);
        distanceTraveled = 0;


        while (distanceTraveled < jumpDist + jumpCooldownDist)
        {
            yield return null;
            distanceTraveled += Time.deltaTime * Terrain.speed;

            if (distanceTraveled < jumpDist)
            {

                yOffset = accel * distanceTraveled * (distanceTraveled - jumpDist);
                yOffset = Mathf.Max(yOffset, 0);
                var newPos = new Vector3(transform.position.x, this.INITIAL_LOCATION.y + yOffset, transform.position.z);
                transform.position = newPos;
            } else {
                var finalPos = new Vector3(transform.position.x, this.INITIAL_LOCATION.y, transform.position.z);
                transform.position = finalPos;
            }
        }
        kidState.isGrounded = true;

    }

	void OnTriggerEnter(Collider other)
	{
		// Debug.Log("Trigger with " +  other.name + " tagged " + other.tag );

		var obs = other.GetComponent<Obstacle>();
		if (obs) {
			Debug.Log("Other is Obstacle");
			ObstacleHit(obs);
		}
	}

	/// <summary>
	/// Handles logic/animation when hitting an obstacle
	/// </summary>
	void ObstacleHit(Obstacle other)
	{
        if (this.kidState.isAlive)
        {
            Destroy(this.transform.Find("Character").gameObject);
            this.kidState.isAlive = false;
            GetComponent<Collider>().enabled = false;
            UpdateUIGameOverText();
        }
	}

    private void UpdateUIGameOverText()
    {
		gameOverPanel.SetActive(true);
        var canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (canvas)
        {
            List<Text> texts = new List<Text>();
            canvas.GetComponentsInChildren<Text>(texts);
            foreach (Text text in texts)
            {
                if (text.name == "GameOverText")
                {
                    text.text = "GAME OVER";
                    break;
                }
            }
        }
    }
}
