using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour
{
    Queue inputKeys = new Queue();
    
	private PlayerPhysics playerPhysics;
	
	private bool hasControl;
	
	public void GiveControl() { hasControl = true; }
	public void RemoveControl() { hasControl = false; }
//	public bool HasControl() { return hasControl; }

	// Use this for initialization
	void Start ()
	{
        //inputKeys.Enqueue(Input.GetKey(KeyCode.RightArrow));
        //inputKeys.Enqueue(Input.GetKey(KeyCode.LeftArrow));

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
			if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				playerPhysics.Slide();
			}
            playerPhysics.GetInput(Input.GetKey(KeyCode.RightArrow), Input.GetKey(KeyCode.LeftArrow));
		}
	}
}
