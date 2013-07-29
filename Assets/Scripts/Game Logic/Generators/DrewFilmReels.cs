using UnityEngine;
using System.Collections.Generic;

public class DrewFilmReels : MonoBehaviour {

    public Transform player;
    public Transform filmReel;
    

    private Transform filmReels;
    public List<GameObject> filmReelParent;
    private GameObject tempParent;

    private int numOfReels;
    public int parentIndex = 0;
    public int numOfObjects = 3;

    public float recycleOffset;
    public float minGap, maxGap, filmReelCount;

    private Vector3 nextPosition;

    private Queue<Transform> objectQueue;
    public float coinCount, randX, randY, randZ, lineGap, coolDown, randomCoins;


    
    // Use this for initialization
    void Awake()
    {
        //instance = this;
        // creates a new queue big as num of objects
        //if (coolDown == 0)
        //{
            objectQueue = new Queue<Transform>(numOfObjects);
            
            //nextPosition = new Vector3(Random.Range(-5, 5), Random.Range(.02f, .80f), Random.Range(10, 16));

            for (int i = 0; i < numOfObjects; i++)
            {
                objectQueue.Enqueue((Transform)filmReel);
                Recycle();
                //parentIndex++;
                //filmReelParent.Add(new GameObject("film reel parent"));
                //print(objectQueue);
            }
    
            // fills up the queue with the prefabs.
          
            //filmReelParent[parentIndex] = tempParent;
        //}
        //else
        //{

        //    coolDown -= Time.deltaTime;
        //}
    }

    void Start()
    {
        minGap = .5f;
        maxGap = 1;
        coinCount = 0;
        randX = 0;
        randY = 0;
        randZ = 0;
        randomCoins = 0;
        lineGap = 0;
        coolDown = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectQueue.Peek().localPosition.z + recycleOffset < player.position.z)
        {
            Recycle();
        }

    }

    private void Recycle()
    {
        //coinCount++;
        //if (coinCount == randomCoins)
        //{
        //    randomCoins = Random.Range(10, 15);
        //    coinCount = 0;
        //}

        //else
        //{
        Transform reel = objectQueue.Dequeue();
        
        randX = Random.Range(-5, 5);
        randY = Random.Range(.2f, .8f);
        //    lineGap = Random.Range(20, 40);
        randZ = Random.Range(10, 20);
        //filmReels.localPosition = new Vector3(randX, randY, randZ);
        //    randZ = 1;
        //    nextPosition.z += randZ;
        //    coinCount++;
        //    objectQueue.Enqueue(filmReels);

        //}
        //else
        //{
        //nextPosition.z += randZ;
        //filmReels.localPosition = new Vector3(randX, randY, nextPosition.z);

        reel.localPosition = nextPosition;
        nextPosition += new Vector3(randX, randY, randZ);
          
          
            objectQueue.Enqueue(reel);
        //}
    }
}
