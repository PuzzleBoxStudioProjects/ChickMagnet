using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour
{
	public static PlayerPhysics instance;
	
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
	public bool hasHitHazard = false;
	
	private Vector3 moveVector;

    //private float characterHeight;
    //private float characterWidth;

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
	}
 
	// Update is called once per frame
	void Update ()
	{
		ResetSlide();
		
		if (GameState.instance.state == GameState.gameStates.caught)
		{
            playerMotor.RemoveControl();
		}
        
        CheckGround();
		HazardControl();		
		ApplyGravity();
	}

	public void ProcessMotion(float dir)
	{
		//move left right
		moveVector = new Vector3(dir * playerSpeed, vertVel, 0);
		rigidbody.velocity = moveVector;
	}
	
	public void Jump()
	{
		//apply jump force
		vertVel = jumpForce * Time.fixedDeltaTime;

		//cancel sliding
		isSliding = false;
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
	
	void HazardControl()
	{
		if (hasHitHazard)
		{
			GameState.instance.hazardHitCnt++;
			hasHitHazard = false;
		}
	}
	
	void ApplyGravity()
	{
		if (!isGrounded && !isSliding)
		{
			vertVel -= gravity * Time.deltaTime;
		}
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

        if (Physics.SphereCast(transform.position, radiusCheck / 2, dir, out hitInfo, distBelowFeet))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
	
	void OnCollisionEnter(Collision col)
	{
		ContactPoint contact = col.contacts[0];
		
		float checkRight = Vector3.Angle(contact.normal, transform.right);
		float checkLeft = Vector3.Angle(contact.normal, -transform.right);

        float checkFront = Vector3.Angle(contact.normal, transform.forward);

		if (checkRight > 100)
		{
			hasHitHazard = true;
		}
		if (checkLeft > 100)
		{
			hasHitHazard = true;
		}
        if (checkFront > 100)
        {
            hasHitHazard = true;

            if (col.transform.tag == "Vehicle")
            {
                GameState.instance.state = GameState.gameStates.caught;
            }
        }
	}
}