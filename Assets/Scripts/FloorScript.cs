using UnityEngine;
using System.Collections;

public class FloorScript : MonoBehaviour
{
	public float levelSpeed = 3.0f;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{		
		this.transform.Translate(Vector3.back * levelSpeed * Time.deltaTime, Space.World); 
	}
}
