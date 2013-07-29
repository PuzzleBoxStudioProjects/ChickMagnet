using UnityEngine;
using System.Collections;
using UnityEditor;

public class ReelDuplicate : MonoBehaviour
{
    public int numOfReels = 20;
    public Transform reels;
    public Transform thisObject;
    public Transform levelObejct;

	// Use this for initialization
	void Awake ()
    {
        DuplicateSelected();
	}

    void Start()
    {
        transform.parent = levelObejct;
    }

    void DuplicateSelected()
    {
        for (int i = 0; i < numOfReels; i++)
        {
            Instantiate(reels);
        }
    }

    void Update()
    {
        if (reels != null)
        {
            reels.parent = thisObject;
        }
    }
}
