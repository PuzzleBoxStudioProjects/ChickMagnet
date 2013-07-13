using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GameLogic : MonoBehaviour
{
	public float distanceTraveled = 0.0f;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        //if (GameState.instance.state != GameState.gameStates.caught)
        //{
        //    distanceTraveled += Time.deltaTime;
        //}
	}
	
	void OnGUI()
	{
        //GUI.Label(new Rect(0, 0, 500, 20), "Distance Traveled " + distanceTraveled.ToString("f1"));
	}
}
