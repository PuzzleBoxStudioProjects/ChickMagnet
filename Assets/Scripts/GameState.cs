using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GameState : MonoBehaviour
{
	public static GameState instance;
	
	public int hazardHitCnt = 0;
	
	public float distanceTraveled = 0.0f;
	public float highScore = 0;
	
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
	
	void Start()
	{
		highScore = PlayerPrefs.GetFloat("Highscore");
	}
	
	// Update is called once per frame
	void Update ()
	{
		//we are still in the game
		if (state != gameStates.caught)
		{
			distanceTraveled += Time.deltaTime;
		}
		//we lost the game so do these things
		else
		{
			//check if high score was surpassed
			if (distanceTraveled > highScore)
			{
				highScore = distanceTraveled;
				
				//round high score to first decimal point
				highScore = Mathf.Round(highScore * 10.0f) / 10.0f;
				//save score
				PlayerPrefs.SetFloat("Highscore", highScore);
			}
		}
		
		//representing each time player collides with a hazard
		if (Input.GetKeyDown(KeyCode.T))
		{
			hazardHitCnt++;
		}
		SetGameStates();
	}
	
	void SetGameStates()
	{
		//set game states that occur when colliding with a hazard
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
	
	void OnGUI()
	{
		GUI.Label(new Rect(0, 30, 500, 20), "High score " + PlayerPrefs.GetFloat("Highscore"));
		GUI.Label(new Rect(0, 0, 500, 20), "Distance Traveled " + distanceTraveled.ToString("f1"));
	}
}
