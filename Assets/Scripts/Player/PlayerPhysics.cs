using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour
{
	public static PlayerPhysics instance;

    public static float distTraveled;

	public float jumpForce = 10.0f;
	public float gravity = 28.0f;
    public float playerSpeed = 10.0f;
	public float amntToRotate = 50.0f;
	public float fallFromSlideForce = 20.0f;

	public float colliderScale = 0.5f;

    //[HideInInspector]
	public bool isGrounded;
	[HideInInspector]
	public bool isSliding;
	
	private PlayerMotor playerMotor;
	
	private float origColliderSizeY;
	private float origColliderCenterY;
	
	public float vertVel;
    public float distBelowFeet = 0.8f;

    public float normLevelSpeed = 10.0f;
    public float stumbleLevelSpeed = 3.0f;
    public float accel = 3.0f;

	public bool hasHitHazard = false;
	
	private Vector3 moveVector;

    public float currentSpeed;

    private float moveDir = 0.0f;
    public float frontDist = 0.5f;
    public GameState.gameStates currentState;

	void Awake()
	{
		instance = this;
	}
	
	// Use this for initialization
	void Start ()
	{
	 	this.rigidbody.freezeRotation = true;
		
		playerMotor = GetComponent<PlayerMotor>();
		
		//log box collider's original size and center point
		origColliderSizeY = ((BoxCollider)collider).size.y;
		origColliderCenterY = ((BoxCollider)collider).center.y;

        this.currentState = GameState.instance.state;
        currentSpeed = MoveSpeed();
	}
 
	// Update is called once per frame
	void Update ()
	{
		ResetSlide();
        CheckGround();
		HazardControl();
        CheckHazard();
        ProcessMotion();
		ApplyGravity();
        SpeedControl();
	}

    void SpeedControl()
    {
        distTraveled = transform.localPosition.z;

        if (this.currentState != GameState.instance.state)
        {
            currentSpeed = MoveSpeed();
            this.currentState = GameState.instance.state;
        }

        if (GameState.instance.state != GameState.gameStates.caught)
        {
            //increase current speed
            if (currentSpeed < normLevelSpeed)
            {
                currentSpeed += accel * Time.deltaTime;
            }
            //reset game state
            else if (currentSpeed > normLevelSpeed)
            {
                GameState.instance.hazardHitCnt = 0;
            }
        }
    }

	void ProcessMotion()
	{
        //game is lost
        if (GameState.instance.state == GameState.gameStates.caught)
        {
            //stop moving
            moveVector.x = 0;
            //take control from player
            playerMotor.RemoveControl();
        }

		//move left right
        moveVector = new Vector3(moveVector.x, vertVel, currentSpeed);
        rigidbody.velocity = moveVector;
	}

    public void GetInput(bool dirR, bool dirL)
    {
        if (dirL && !dirR)
        {
            moveDir = -1;
        }
        if (dirR && !dirL)
        {
            moveDir = 1;
        }
        if (!dirL && !dirR)
        {
            moveDir = 0;
        }
        moveVector.x = playerSpeed * moveDir;
    }

	public void Jump()
	{
        //cancel sliding
		isSliding = false;
		//apply jump force
		vertVel = jumpForce * Time.fixedDeltaTime;		
	}
	
	public void Slide()
	{
		//get box collider
		BoxCollider boxCol = (BoxCollider)collider;
		
		//get box collider's center point and size
		Vector3 boxCenter = boxCol.center;
		Vector3 boxSize = boxCol.size;
		
		//scale the size
		boxSize.y = origColliderSizeY * colliderScale;
		//set the center point so the pivot point stays in its original position
		boxCenter.y = origColliderCenterY - (origColliderSizeY * (1 - colliderScale)) * 0.5f;
		
		//set the size to actually change the scale and apply center point
		boxCol.size = boxSize;
		boxCol.center = boxCenter;
		
        //quickly put the player down to the ground
		if (!isGrounded)
		{
			vertVel = -fallFromSlideForce;
		}
		
		//wait a time before resetting size
		StartCoroutine("WaitAndSlide", 2);

		isSliding = true;
	}
	
	void ResetSlide()
	{
		//get box collider
		BoxCollider boxCol = (BoxCollider)collider;
		
		//get box collider's center point and size
		Vector3 boxCenter = boxCol.center;
		Vector3 boxSize = boxCol.size;
		
		if (!isSliding)
		{
			//reset size and center point
			boxSize.y = origColliderSizeY;
			boxCenter.y = origColliderCenterY;
			
			boxCol.size = boxSize;
			boxCol.center = boxCenter;
		}
	}
	
	IEnumerator WaitAndSlide(float waitTime)
	{
		//wait
		yield return new WaitForSeconds(waitTime);
		isSliding = false;
	}

    float MoveSpeed()
    {
        float levelSpeed = 0.0f;

        //set level speeds
        switch (GameState.instance.state)
        {
            case GameState.gameStates.safe:
                levelSpeed = normLevelSpeed;
                break;
            case GameState.gameStates.stumble:
                levelSpeed = stumbleLevelSpeed;
                break;
            case GameState.gameStates.caught:
                levelSpeed = 0;
                break;
        }
        return levelSpeed;
    }

    void HazardControl()
    {
        //hit a hazard so add one
        if (hasHitHazard)
        {
            GameState.instance.hazardHitCnt++;
            hasHitHazard = false;
        }
    }

    void CheckHazard()
    {
        RaycastHit hitInfo;

        float radiusCheck = collider.bounds.size.y;

        Vector3 dir = transform.forward;

        //hitting something below
        if (Physics.SphereCast(transform.position + transform.up * 1.4f, radiusCheck, dir, out hitInfo, frontDist))
        {
            //instant kill
            if (hitInfo.transform.tag == "Instakill")
            {
                GameState.instance.hazardHitCnt = 2;
            }
            else
            {
                hasHitHazard = true;
            }
        }
    }
    	
	void ApplyGravity()
	{
        //apply gravity
		if (!isGrounded && !isSliding)
		{
			vertVel -= gravity * Time.deltaTime;
		}
        //apply a small downward force to stay grounded
		if (isGrounded)
		{
			vertVel = -1;
		}
	}

    void CheckGround()
    {
        RaycastHit hitInfo;

        float radiusCheck = collider.bounds.extents.y;
        
        Vector3 dir = Vector3.down;

        //hitting something below
        if (Physics.SphereCast(transform.position, radiusCheck * 0.5f, dir, out hitInfo, distBelowFeet + radiusCheck * 0.5f))
        {
            isGrounded = true;
        }
            //not hitting something below
        else
        {
            isGrounded = false;
        }
    }
	
	void OnCollisionEnter(Collision col)
	{
		ContactPoint contact = col.contacts[0];
		
        //check sides
		float checkRight = Vector3.Angle(contact.normal, transform.right);
		float checkLeft = Vector3.Angle(contact.normal, -transform.right);
        
        //check in front of player
        float checkFront = Vector3.Angle(contact.normal, transform.forward);

        //has hit left or right side
		if (checkRight > 100 || checkLeft > 100)
		{
            if (col.transform.tag == "Wall")
            {
                //instant kill
                GameState.instance.hazardHitCnt = 2;
            }
            else
            {
                hasHitHazard = true;
            }
		}
        ////has hit in front
        //if (checkFront > 100)
        //{
        //    //instant kill
        //    if (col.transform.tag == "Instakill")
        //    {
        //        GameState.instance.hazardHitCnt = 2;
        //    }
        //    else
        //    {
        //        hasHitHazard = true;
        //    }
        //}
	}
}