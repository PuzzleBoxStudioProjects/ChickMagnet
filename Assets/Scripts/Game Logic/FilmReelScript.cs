using UnityEngine;
using System.Collections.Generic;

public class FilmReelScript : MonoBehaviour {
	public static FilmReelScript instance;
	public Transform player;
    public Transform filmReel;
    public Transform levelObejct;
	private Transform filmReels;
    public int numOfobjects;
    public float recycleOffset;
  	public float minGap, maxGap, filmReelCount;
    private Vector3 nextPosition;
    private Queue<Transform> objectQueue;
   
	
	
	void Start(){
		minGap = 20;
		maxGap = 60;
		
	}
    // Use this for initialization
    void Awake()
    {
		instance = this;
        // creates a new queue big as num of objects
        objectQueue = new Queue<Transform>(numOfobjects);
        // fills up the queue with the prefabs.
        for (int i = 0; i < numOfobjects; i++)
        {
            objectQueue.Enqueue((Transform)Instantiate(filmReel));
        }
        
         nextPosition = new Vector3(Random.Range(-7,7),Random.Range(.02f,.80f), Random.Range(-20,-10));
		for (int i = 0; i < numOfobjects; i++)
        {
            Recycle();
        }
    }

    // Update is called once per frame
    public void Update()
    {
		if (objectQueue.Peek().localPosition.z + recycleOffset < player.position.z)
        {
            Recycle();
        }
		
    }

    private void Recycle()
    {	float offset = Random.Range(minGap, maxGap);
		
        filmReels = objectQueue.Dequeue();
        filmReels.parent = levelObejct;
        filmReels.localPosition = new Vector3(Random.Range(-7,7),Random.Range(.02f,.80f), nextPosition.z);
        nextPosition.z += offset;
        
        objectQueue.Enqueue(filmReels);
		
    }
}
