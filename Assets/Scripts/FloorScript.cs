using UnityEngine;
using System.Collections;

public class FloorScript : MonoBehaviour
{
	public float normLevelSpeed = 8.0f;
	public float stumbleLevelSpeed = 3.0f;
	public float currentSpeed;
	
	public GameState.gameStates currentState;
	
	// Use this for initialization
	void Start ()
	{
		this.currentState = GameState.instance.state;
		currentSpeed = MoveSpeed();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.currentState != GameState.instance.state)
		{
			currentSpeed = MoveSpeed();
			this.currentState = GameState.instance.state;
		}
		
		//move level
		this.transform.Translate(Vector3.back * currentSpeed * Time.deltaTime, Space.World);
		
		if (this.currentState != GameState.gameStates.caught)
		{
			//increase current speed
			if (currentSpeed < normLevelSpeed)
			{
				currentSpeed += normLevelSpeed * Time.deltaTime;
			}
			//reset game state
			else if (currentSpeed >= normLevelSpeed)
			{
//				GameState.instance.state = GameState.gameStates.safe;
				GameState.instance.hazardHitCnt = 0;
			}
		}
	}
	
	float MoveSpeed()
	{
		float levelSpeed = 0.0f;
		
		//set level speeds
		switch(GameState.instance.state)
		{
		case GameState.gameStates.safe:
			levelSpeed = normLevelSpeed;
			break;
		case GameState.gameStates.stumble:
			levelSpeed = stumbleLevelSpeed;
			break;
		case GameState.gameStates.caught:
			levelSpeed = 0;
			break;
		}
		return levelSpeed;
	}
}
