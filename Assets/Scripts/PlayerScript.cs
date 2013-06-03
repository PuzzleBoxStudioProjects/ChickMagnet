using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	 public int jumpSpeed;
	
	public float playerSpeed = 10.0f;
	 public float amntToRotate = 50.0f;
		
//	 [HideInInspector]
	 public bool isGrounded;
	 [HideInInspector]
	 public bool isSliding;
	 [HideInInspector]
	 public bool isJumping;
	 
	public Vector3 oldPos;
	
	 // Use this for initialization
	 void Start ()
	 {
		oldPos = transform.position;
	 	this.rigidbody.freezeRotation = true;
	 }
 
	 // Update is called once per frame
	 void FixedUpdate ()
	 {
		ProcessMotion();
   	}
	
	void ProcessMotion()
	{
		var amtToJump = jumpSpeed * Time.deltaTime;
		float dir = Input.GetAxisRaw("Horizontal");
		
		if(Input.GetKey(KeyCode.UpArrow) && !isJumping && !isSliding)
		{
			  this.transform.Translate(Vector3.up * amtToJump);
			  isJumping = true;
			  isGrounded = false;
		}
		
		if (!isSliding)
		{
			transform.Translate(Vector3.right * dir * playerSpeed * Time.deltaTime);
		}
		
	 	BoxCollider boxCol = this.transform.collider as BoxCollider;
			
		if(Input.GetKey(KeyCode.DownArrow) && isGrounded)
		{
			oldPos = transform.position;
		 	if (boxCol != null)
			{
				boxCol.size = new Vector3(1, 0.5f, 1);
				StartCoroutine("WaitAndSlide", 2);
			}
			isSliding = true;
	  	}
		
		if (!isSliding)
		{
			boxCol.size = new Vector3(1, 1, 1);
		}
	}
	
	IEnumerator WaitAndSlide(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		transform.position = oldPos;
		isSliding = false;
	}
	
	void OnCollisionEnter(Collision col)
	{
		ContactPoint contact = col.contacts[0];
		
		float checkBelow = Vector3.Angle(contact.normal, -transform.up);
		
		if (checkBelow > 60)
		{
			isJumping = false;
			isGrounded = true;
		}
	}
}