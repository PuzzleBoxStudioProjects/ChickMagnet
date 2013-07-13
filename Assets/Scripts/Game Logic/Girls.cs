using UnityEngine;
using System.Collections;

public class Girls : MonoBehaviour
{
    public float chaseSpeed = 5.0f;

    public Transform safeTarget;
    public Transform chaseTarget;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameState.instance.state == GameState.gameStates.stumble)
        {
            transform.position = Vector3.MoveTowards(transform.position, chaseTarget.position, chaseSpeed * Time.deltaTime);
        }
        else if (GameState.instance.state == GameState.gameStates.safe)
        {
            transform.position = Vector3.MoveTowards(transform.position, safeTarget.position, chaseSpeed * Time.deltaTime);
        }
        else
        {
            //perform glomp animation
        }
	}
}
