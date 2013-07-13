using UnityEngine;
using System.Collections;

public class CamScript : MonoBehaviour
{
	public float camPosY = 10.0f;
    public float zDistance = 12.0f;
	public float camSmooth = 3.0f;
	
	public GameObject player;
	
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindWithTag("Player");
	}
	
	void FixedUpdate ()
	{		
//		if (PlayerScript.instance.isGrounded)
//		{
			transform.position = new Vector3(transform.position.x,
				Mathf.Lerp(transform.position.y, player.transform.position.y + camPosY, camSmooth * Time.deltaTime),
				player.transform.position.z - zDistance);
//		}
	}
}
