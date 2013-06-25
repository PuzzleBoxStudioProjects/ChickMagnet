using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour
{
	private PlayerPhysics playerPhysics;
	
	private bool hasControl;
	
	public void GiveControl() { hasControl = true; }
	public void RemoveControl() { hasControl = false; }
//	public bool HasControl() { return hasControl; }
	
	// Use this for initialization
	void Start ()
	{
		playerPhysics = GetComponent<PlayerPhysics>();
		
		hasControl = true;
	}
	
	void FixedUpdate ()
	{
		if (hasControl)
		{
			//jump
			if(Input.GetKey(KeyCode.UpArrow) && playerPhysics.isGrounded)
			{
				playerPhysics.Jump();
			}
			//slide
			if(Input.GetKey(KeyCode.DownArrow) && playerPhysics.isGrounded)
			{
				playerPhysics.Slide();
			}
			playerPhysics.ProcessMotion(Input.GetAxisRaw("Horizontal"));
		}
	}
}
