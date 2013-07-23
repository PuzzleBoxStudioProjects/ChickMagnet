using UnityEngine;
using System.Collections.Generic;

public class filmreelscript2 : MonoBehaviour {
	public static filmreelscript2 instance;
	
	public Transform player;
    public Transform filmReel;
    public Transform collectableObject;
	private Transform filmReels;
    
	public int numOfobjects;
    
	public float recycleOffset;
  	public float minGap, maxGap, filmReelCount;
    
	private Vector3 nextPosition;
    
	private Queue<Transform> objectQueue;
    private float coinCount, randX, randY, randZ, lineGap, coolDown,randomCoins;
	
	
	void Start(){
		minGap = .5f;
		maxGap = 1;
		coinCount = 0;
		randX = 3;
		randY = 0;
		randZ = 1;
		randomCoins = 5;
		lineGap = 0;
		coolDown = 10;
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
	int RandomFriends(int min, int max){
		return Random.Range (min,max);		
	}
	
    private void Recycle()
    {	
		filmReels = objectQueue.Dequeue();
        filmReels.parent = collectableObject;
		nextPosition.z = RandomFriends((int)minGap,(int)maxGap);
		
		if(coinCount == randomCoins){
			filmReels.localPosition = new Vector3(RandomFriends(-5,5), 1, nextPosition.z);
			randomCoins = RandomFriends(5,15);
			coinCount = 0;
			objectQueue.Enqueue(filmReels);
			
		}
		else {
			coinCount++;
			filmReels.localPosition = new Vector3(RandomFriends(-5,5), 1, nextPosition.z +=1);
			objectQueue.Enqueue(filmReels);
		}
		
		
		
		
		

   }
}
