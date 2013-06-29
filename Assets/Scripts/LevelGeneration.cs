using UnityEngine;
using System.Collections.Generic;

public class LevelGeneration : MonoBehaviour {
	public Transform prefab;
	public Transform thisprefab;
	public int numOfobjects;
	public float recycleOffset;
	public float distanceCheck;
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	// Use this for initialization
	void Start () {
		objectQueue = new Queue<Transform>(numOfobjects);
		nextPosition = transform.localPosition;
		for(int i = 0; i < numOfobjects; i++){
			objectQueue.Enqueue((Transform)Instantiate(prefab));
		}
		nextPosition = transform.localPosition;
		for(int i = 0; i < numOfobjects; i++){
			Recycle();
		}
	}
	
	// Update is called once per frame
	public void Update () {
		 distanceCheck = objectQueue.Peek().localPosition.z + recycleOffset;
		if(objectQueue.Peek().localPosition.z + recycleOffset < -5){
			distanceCheck = 0;
			Recycle ();
		}
	}
	private void Recycle(){
			Transform road = objectQueue.Dequeue();
			road.parent = thisprefab;
            
			road.position = new Vector3(nextPosition.x,nextPosition.y,nextPosition.z);
			nextPosition.z += 10;
			objectQueue.Enqueue(road);
	}
}
