using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour
{
	public static PlayerPhysics instance;
	
	public float jumpForce = 10.0f;
	
	public float playerSpeed = 10.0f;
	public float amntToRotate = 50.0f;
	public float colliderScale = 0.5f;
	
	[HideInInspector]
	public bool isGrounded;
	[HideInInspector]
	public bool isSliding;
	
	private PlayerMotor playerMotor;
	
	private float origColliderSizeY;
	private float origColliderCenterY;
	
	private Vector3 moveVector;
	
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
	}
	
	public void ProcessMotion(float dir)
	{
		//move left right
		moveVector = new Vector3(dir * playerSpeed, rigidbody.velocity.y, 0);
		rigidbody.velocity = moveVector;
	}
	
	public void Jump()
	{
		rigidbody.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
			
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
		
		//scale the size to colliderScale value
		boxSize.y = origColliderSizeY * colliderScale;
		//set the center point so the pivot point stays in its original position
		boxCenter.y = origColliderCenterY - (origColliderSizeY * (1 - colliderScale)) * 0.5f;
		
		boxCol.size = boxSize;
		boxCol.center = boxCenter;
		
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
	
	//check for grounded
	void OnCollisionStay(Collision col)
	{
		//get first contact point
		ContactPoint contact = col.contacts[0];
		
		//get the angle below
		float checkBelow = Vector3.Angle(contact.normal, -transform.up);
		
		//any number below 180 to check for ground
		if (checkBelow > 60)
		{
			isGrounded = true;
		}
	}
	
	void OnCollisionExit()
	{
		isGrounded = false;
	}
}