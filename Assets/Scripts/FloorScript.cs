using UnityEngine;
using System.Collections;

public class FloorScript : MonoBehaviour
{
    public static FloorScript instance;

	public float normLevelSpeed = 8.0f;
	public float stumbleLevelSpeed = 3.0f;
	public float accel = 3.0f;

    public float distTraveled;
    public float lastPosit;

	private float currentSpeed;

    public static Transform levelParent;

	public GameState.gameStates currentState;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start ()
	{
        levelParent = this.transform;
		this.currentState = GameState.instance.state;
		currentSpeed = MoveSpeed();
	}
	
	// Update is called once per frame
	void Update ()
	{
        lastPosit = this.transform.position.z;
        distTraveled = lastPosit - this.transform.position.z;

		if (this.currentState != GameState.instance.state)
		{
			currentSpeed = MoveSpeed();
			this.currentState = GameState.instance.state;
		}
		
		//move level
		this.transform.Translate(Vector3.back * currentSpeed * Time.deltaTime, Space.World);

        if (GameState.instance.state != GameState.gameStates.caught)
        {
            //increase current speed
            if (currentSpeed < normLevelSpeed)
            {
                currentSpeed += accel * Time.deltaTime;
            }
            //reset game state
            else if (currentSpeed >= normLevelSpeed)
            {
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
