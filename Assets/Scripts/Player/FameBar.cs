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
        if (hasCollected)
        {
            if (reelCount < maxReelCount)
            {
                reelCount++;
            }

            CancelInvoke();
            hasCollected = false;
        }
        else if (!IsInvoking("LoseReels") && reelCount > 0)
        {
            Invoke("LoseReels", 1.0f);
        }
    }

    void LoseReels()
    {
        reelCount -= 2;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "filmReels")
        {
            hasCollected = true;
            DrewFilmReels.instance.Recycle();
        }
    }
}
