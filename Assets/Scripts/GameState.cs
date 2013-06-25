using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{
	public static GameState instance;
	
	public int hazardHitCnt = 0;
	
	//list of game states
	public enum gameStates
	{
		safe,
		stumble,
		caught
	}
	
	//getter setter for game states
	public gameStates state { get; set; }
	
	void Awake ()
	{
		instance = this;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//representing each time player collides with a hazard
		if (Input.GetKeyDown(KeyCode.T))
		{
			hazardHitCnt++;
		}
		switch(hazardHitCnt)
		{
		case 0:
			state = gameStates.safe;
			break;
		case 1:
			state = gameStates.stumble;
			break;
		case 2:
			state = gameStates.caught;
			break;
		}
	}
}
