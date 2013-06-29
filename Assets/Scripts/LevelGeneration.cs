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
	void Awake() {
		objectQueue = new Queue<Transform>(numOfobjects);
		for(int i = 0; i < numOfobjects; i++){
			objectQueue.Enqueue((Transform)Instantiate(prefab));
		}
		nextPosition = transform.localPosition * .5f;
		for(int i = 0; i < numOfobjects; i++){
			Recycle();
		}
	}
	
	// Update is called once per frame
	public void Update () {
		if(objectQueue.Peek().localPosition.z + recycleOffset < 0){
			Recycle ();
		}
	}
	private void Recycle(){
		Transform road = objectQueue.Dequeue();
		road.parent = thisprefab;
		road.localPosition = nextPosition ;
		nextPosition.z += road.localScale.z;		
		objectQueue.Enqueue(road);
	}
}
