using UnityEngine;
using System.Collections;

public class FameBar : MonoBehaviour
{
    /*every reel collected add one second to power clock
    every second not collected two seconds are taken away
     * until reaching zero
     * 1:30 ratio.  every 30 increment a girl goes away
     * */
    
    public bool hasCollected = false;
    
    [HideInInspector]
    public int reelCount = 0;

    private int maxReelCount = 180;
        
	// Update is called once per frame
	void Update ()
    {
        ReelCollection();
	}

    void ReelCollection()
    {
        //we've collected
        if (hasCollected)
        {
            if (reelCount < maxReelCount)
            {
                //add one
                reelCount++;
            }
            //stop taking away
            CancelInvoke();
            //reset boolean
            hasCollected = false;
        }
            //not collecting
        else if (!IsInvoking("LoseReels") && reelCount > 0)
        {
            //take away every set seconds
            Invoke("LoseReels", 1.0f);
        }
    }

    void LoseReels()
    {
        //take away
        reelCount -= 2;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "filmReels")
        {
            hasCollected = true;
            //recycle the reel when colliding to simulate pick up
            DrewFilmReels.instance.Recycle();
        }
    }
}
