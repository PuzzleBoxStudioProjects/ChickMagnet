using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	public static PlayerScript instance;
	
	public float jumpForce = 10.0f;
	
	public float playerSpeed = 10.0f;
	public float amntToRotate = 50.0f;
	public float colliderScale = 0.5f;
	
	[HideInInspector]
	public bool isGrounded;
	[HideInInspector]
	public bool isSliding;
	[HideInInspector]
	public bool isJumping;
	
	private float origColliderSizeY;
	private float origColliderCenterY;
	
	void Awake()
	{
		instance = this;
	}
	
	 // Use this for initialization
	 void Start ()
	 {
	 	this.rigidbody.freezeRotation = true;
		
		//log box collider's original size and center point
		origColliderSizeY = ((BoxCollider)collider).size.y;
		origColliderCenterY = ((BoxCollider)collider).center.y;
	 }
 
	 // Update is called once per frame
	 void FixedUpdate ()
	 {
		ProcessMotion();
   	}
	
	void ProcessMotion()
	{
		float dir = Input.GetAxisRaw("Horizontal");
		
		//jump
		if(Input.GetKey(KeyCode.UpArrow) && isGrounded)
		{
			rigidbody.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
//			this.transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
			isJumping = true;
			isGrounded = false;
		}
		
		//move left right
		this.transform.Translate(Vector3.right * dir * playerSpeed * Time.deltaTime);
		
		//get box collider
		BoxCollider boxCol = (BoxCollider)collider;
		
		//get box collider's center point and size
		Vector3 boxCenter = boxCol.center;
		Vector3 boxSize = boxCol.size;
		
		//slide
		if(Input.GetKey(KeyCode.DownArrow) && isGrounded)
		{
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
		
		if (!isSliding || isJumping)
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
	
	void OnCollisionEnter(Collision col)
	{
		//get first contact point
		ContactPoint contact = col.contacts[0];
		
		//get the angle below
		float checkBelow = Vector3.Angle(contact.normal, -transform.up);
		
		//any number below 180 to check for ground
		if (checkBelow > 60)
		{
			isJumping = false;
			isGrounded = true;
		}
	}
}