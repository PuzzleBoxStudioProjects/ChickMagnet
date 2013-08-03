using UnityEngine;
using System.Collections.Generic;

public class DrewFilmReels : MonoBehaviour
{
    public static DrewFilmReels instance;

    public Transform player;
    public Transform filmReel;
    public Transform levelObejct;
    private Transform filmReels;

    public int numOfobjects;

    public float recycleOffset;
    public float minGap, maxGap, filmReelCount;

    private Vector3 nextPosition;

    private Queue<Transform> objectQueue;
    private float coinCount, randX, randY, randZ, lineGap, coolDown, randomCoins;

    private FameBar fameBar;

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

        nextPosition = new Vector3(Random.Range(-5, 5), Random.Range(.02f, .80f), player.position.z + 50);
        for (int i = 0; i < numOfobjects; i++)
        {
            Recycle();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (objectQueue.Peek().localPosition.z + recycleOffset < player.position.z)
        {
            Recycle();
        }

    }

    public void Recycle()
    {
        if (coinCount == randomCoins)
        {
            randomCoins = Random.Range(5, 15);
            coinCount = 0;
        }

        filmReels = objectQueue.Dequeue();
        filmReels.parent = levelObejct;


        if (coinCount == 0)
        {
            randX = Random.Range(-5, 5);
            randY = Random.Range(.2f, .8f);
            lineGap = Random.Range(80, 120);
            randZ = Random.Range(25, 50);
            filmReels.localPosition = new Vector3(randX, randY, randZ);
            randZ = 1;
            nextPosition.z += randZ; ;
            coinCount++;
            objectQueue.Enqueue(filmReels);

        }
        else
        {
            filmReels.localPosition = new Vector3(randX, randY, nextPosition.z);
            nextPosition.z += randZ; ;
            coinCount++;
            objectQueue.Enqueue(filmReels);
        }
    }
}
