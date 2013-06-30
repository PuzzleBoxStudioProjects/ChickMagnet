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
	private int i;
	// Use this for initialization
	void Awake() {
		i = 0;
		// creates a new queue big as num of objects
		objectQueue = new Queue<Transform>(numOfobjects);
		// fills up the queue with the prefabs.
		//for(int i = 0; i < numOfobjects; i++){
		//	objectQueue.Enqueue((Transform)Instantiate(prefab));
		//}
		nextPosition.z = transform.localPosition.z * .5f ;
		//for(int i = 0; i < numOfobjects; i++){
		//	Recycle();
		//}
	}
	
	// Update is called once per frame
	public void Update () {
		if(i < numOfobjects){
			objectQueue.Enqueue((Transform)Instantiate(prefab));
			i++;
		}
		if(objectQueue.Peek().localPosition.z  < -5){
			Recycle ();
		}
	}
	private void Recycle(){
		Transform road = objectQueue.Dequeue();
		road.parent = thisprefab;
		road.localPosition += nextPosition ;
		nextPosition.z += road.localScale.z * .5f ;		
		objectQueue.Enqueue(road);
	}
}
