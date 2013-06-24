using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{
	public static GameState instance;
	
	//list of game states
	public enum gameStates
	{
		safe,
		stumble,
		caught
	}
	
	//getter setter for game states
	public gameStates state { get; set; }
	
	// Use this for initialization
	void Awake ()
	{
		instance = this;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			state = gameStates.safe;
		}
		//player hit something to stumble
		if (Input.GetKeyDown(KeyCode.Y))
		{
			state = gameStates.stumble;
		}
		if (Input.GetKeyDown(KeyCode.U))
		{
			state = gameStates.caught;
		}
	}
}
